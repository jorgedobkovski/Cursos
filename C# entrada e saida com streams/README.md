# Entrada e saída (I/O) com streams

# Fluxo de dados

> A ideia que estamos trabalhando é a seguinte: **não lidamos com o arquivo completo para exibir dados ao usuário, e sim com fluxo de dados**. Isso vale para um vídeo na internet, um filme em 4K e também para os arquivos de texto que usaremos em nosso projeto.
> 

# Lendo um arquivo de texto

É necessário informar o arquivo que será utilizado com seu diretório.

```csharp
var arquivo = "contas.txt";
```

Assim feito, precisamos criar o fluxo de bytes que possibilitará o acesso ao arquivo `conta.txt`, para então podermos percorrer os bytes que o definem. Criaremos uma nova variável chamada `fluxoDoArquivo`, que receberá o fluxo, em inglês, *stream*. Contudo não estamos nos referindo a qualquer tipo de *stream*, assim como temos um fluxo de rede na internet, temos o fluxo de arquivos, portanto escreveremos `FileStream`.

```csharp
namespace ByteBankImportacaoExportacao
{

    class Program
    {
        static void Main(string[] args)
        {
            var enderecoDoArquivo = "/contas.txt";

            var fluxoDoArquivo = new FileStream(enderecoDoArquivo, FileMode.Open);

            var buffer = new byte[1024]; // 1 kb

            fluxoDoArquivo.Read(buffer,0,1024);

            Console.ReadLine();
        }
    }
}
```

Agora escreveremos o que foi coletado desse arquivo. Para deixar o código mais elegante criaremos um método especializado em escrever o `buffer` na tela, que será o `EscreverBuffer()`
 e receberá como argumento um `byte[] buffer`.

```csharp
static void EscreverBuffer(byte[] buffer)
        {
            foreach (var meuByte in buffer)
            {
                Console.Write(meuByte);
                Console.Write(" ");
            }
        }
```

> **Devoluções do método .Read():**
O número total de bytes lidos do buffer. Isso poderá ser menor que o número de bytes solicitado se esse número de bytes não estiver disponível no momento, ou zero, se o final do fluxo for atingido
> 

Precisaremos ler o arquivo até que ele chegue ao seu final, ou seja, até que o número de bytes lido ser zero. Portanto, usaremos o laço `while(numeroDeBytesLidos !=0`, enquanto o número de bytes lidos for diferente de zero, receberemos na variável `numeroDeBytesLidos` quantos bytes foram lidos e faremos o processamento, no caso, simplesmente escrever o resultado na tela.

```csharp
class Program
    {
        static void Main(string[] args)
        {
            var buffer = new byte[1024]; // 1 kb
            var numeroDeBytesLidos = -1;

            while(numeroDeBytesLidos !=0)
            {
                numeroDeBytesLidos = fluxoDoArquivo.Read(buffer,0,1024);
                EscreverBuffer(buffer);

            }
            Console.ReadLine();
        }

        static void EscreverBuffer(byte[] buffer)
        {
            foreach (var meuByte in buffer)
            {
                Console.Write(meuByte);
                Console.Write(" ");
            }
        }
    }

```

# Encoding

Como podemos usar esse recurso em nossa aplicação? Sabemos que o byte não representa um caractere diretamente, portanto precisaremos de um codificador/decodificador para realizar a transformação. Temos no .NET a classe abstrata do tipo `Encoding`, por isso não precisamos utilizar o `new`.

Vamos averiguar se `UTF-8` consue fazer a sugestão dos bytes que nós temos para o texto. A partir da referência `utf8`, queremos obter uma string, por isso usaremos o método `GetString()`, que espera receber como argumento um array de bytes, portanto passaremos `buffer`, que contém os bytes do arquivo. Tudo isso será guardado dentro da variável `texto`. Em seguida, escreveremos na tela (`Console.Write(texto)`).

```csharp
static void EscreverBuffer(byte[] buffer)
        { 
            var encoding = Encoding.Default;
            var texto = encoding.GetString(buffer);
            
            Console.Write(texto);
        }
```

Assim feito, iniciaremos a aplicação. Teremos como resultado não uma série de números, mas os caracteres que os bytes representam.

# Método Close

Se tentarmos renomear o arquivo contas.txt com nosso programa aberto o sistema não irá permitir, mesmo ele já tendo lido todo o arquivo.

A aplicação não liberou o recurso pelo sistema operacional. Em nosso código, utilizamos o `buffer`, concluímos o processamento e depois utilizamos `Console.ReadLine()`. Em nenhum momento notificamos o sistema operacional que o uso do arquivo foi terminado. Para isso, escreveremos  `fluxoDoArquivo` associado ao método `Close()`, que indicará o fechamento do arquivo.

```csharp
while(numeroDeBytesLidos != 0)
            {
                numeroDeBytesLidos = fluxoDoArquivo.Read(buffer, 0, 1024);
                EscreverBuffer(buffer);
            }

                        fluxoDoArquivo.Close();

            Console.ReadLine();
        }
```

Agora é possível renomear o arquivo.

## Using

> Como o uso do método `Close()` é obrigatório, precisamos nos atentar às exceções que podem ocorrer em nosso código, pois em caso alguma exista, a chamada para `Close()` não acontecerá e aplicação reterá o recurso. Como a classe `FileStream` implementa a interface `IDisposable` é possível utilizar o padrão Using, ao invés de criar um try catch finally.
> 

```csharp
    var enderecoDoArquivo = "contas.txt";

    using(var fluxoDoArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
    {
        var buffer = new byte[1024] // 1 kb
        var numeroDeBytesLidos = -1;

        while (nmeriDeBytesLidos !=0)
        {
        numeroDeBytesLidos = fluxoDoArquivo.Read(buffer, 0, 1024);
        EscreverBudder(buffer);
        }
    }

    Console.ReadLine();
}
```

Não estamos explicitamente evocando o método `Close()` . Mas o `Dispose()`, que internamente chamará o método `Close()`.

# Cuidados em trabalhar com buffer

> Percebemos que o conteúdo de nosso arquivo está sendo duplicado na Console. Isto acontece porque o `Read` só escreve em nosso buffer os bytes lidos. Ao chegar no final do arquivo, nosso Stream recupera menos de 1024 bytes e então ficamos com dados antigos no buffer. Para resolver esse problema, precisamos indicar ao método `EscreverBuffer` quantos bytes de nosso buffer devem ser processados.
> 

```csharp
partial class Program
    {
        static void LidandoComFileStreamDiretamente()
        {
            var arquivo = "contas.txt";
            using (var fluxoDoArquivo = new FileStream(arquivo, FileMode.Open))
            {
                var buffer = new byte[1024];
                var quantidadeBytesLidos = -1;

                while (quantidadeBytesLidos != 0)
                {
                    quantidadeBytesLidos = fluxoDoArquivo.Read(buffer, 0, 1024);
                    EscreverBuffer(buffer, quantidadeBytesLidos); 
										//passando quantidade de bytes lidos no metodo escrever buffer
                }
            }

            Console.ReadLine();
        }

        static void EscreverBuffer(byte[] buffer, int bytesLidos)
        {
            var encoding = Encoding.Default;
            var texto = encoding.GetString(buffer, 0, bytesLidos);
						//usando outra sobrecarga do método GetString()

            Console.Write(texto);
        }
    }
```

Ficou evidente, então, que quando trabalhamos com buffers é necessário bastante cuidado com os intervalos utilizado.

# StreamReader

Ao invés de lidar diretamente com o `FileStream` e bytes de Stream, podemos usar uma classe que encapsula esta lógica, o `StreamReader`:

```csharp
var leitor = new StreamReader(fluxoDeArquivo)
```

Usamos o `ReadLine` para a leitura de uma linha do arquivo e a propriedade `EndOfStream` para verificar se chegamos ao fim do stream:

```csharp
using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
using (var leitor = new StreamReader(fluxoDeArquivo))
{
    while (!leitor.EndOfStream)
    {
        var linha = leitor.ReadLine();
        Console.WriteLine(linha);
    }
}
```

> Verifique também os métodos `ReadToEnd` e `Read` da classe `StreamReader`. Enquanto o  `ReadToEnd` retorna o arquivo completo, o `Read` retorna apenas um byte.
> 
> 
> Perceba que apesar do retorno de `Read` ser um char do arquivo, seu tipo de retorno é `int` e não `byte` ou `char`. Isto é porque o método retorna `-1` quando o fim do stream foi atingido.
> 

# Convertendo strings lidas para objetos

> Agora que trabalhamos em como recuperar o arquivo do HD por meio do stream, precisamos converter a string para uma conta corrente. Em `Program.cs`, criaremos um método `static ContaCorrente ConverterStringParaContaCorrente`, que receberá como argumento uma `string linha`.
> 

Antes de escrevermos este código, analisaremos o arquivo `conta.txt`
 para definir qual tipo de lógica deverá ser aplicada. Cada linha possui quatro campos: os três primeiros são **agência**, os outros quatro são **número da conta** e a última série corresponde o **saldo**, seguir por titular.

```
375 4644 2483.13 Jonatan
```

Tendo isso em vista, coletaremos cada linha e a quebraremos, usando o caractere de espaço como marca de divisão dos conteúdos. Para fragmentarmos uma string por um caractere específico, usamos o método `Split()`, que por sua vez receberá neste caso o caractere espaço. Assim feito, associaremos esse método com `linha`. Criaremos uma variável `campos` que armazenará esses conteúdos.

Resta separmos os campos em `agencia`, `conta`, `saldo` e `titular`, com seus respectivos índices. Em seguida, criaremos a `ContaCorrente` como resultado, que receberá como argumento `agencia`  e `numero`. Fazendo as conversões e ajustes necessários.

```csharp
static ContaCorrente ConverterStringParaContaCorrente(string linha)
{
       string[] campos = linha.Split(' ');

       var agencia = campos[0];
       var numero = campos[1];
       var saldo = campos[2].Replace('.', ' ,');
       var nomeTitular = campos[3];

       var agenciaComInt = int.Parse(agencia);
       var numeroComInt = int.Parse(numero);
       var saldoComoDouble = double.Parse(saldo);

       var titular = new Cliente();
       titular.Nome = nomeTitular;

       var resultado = new ContaCorrente(agenciaComInt, numeroComInt);
       resultado.Depositar(saldoComoDouble);
       resultado.Titular = titular;

       return resultado;
}
```

Para termos certeza de que a aplicação está operando, comentaremos a linha  `Console.WriteLine(linha)`, e escreveremos o resultado da `ContaCorrente`, com  `Numero` , `Agencia` e `Saldo`, que guardaremos em uma variável `msg`. Por fim, imprimiremos o resultado na tela.

```csharp
using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
        using (var leitor = new StreamReader(fluxoDeArquivo))
        {
            while (!leitor.EndOfStream)
            {
                var linha = leitor.ReadLine();
                var contaCorrente = ConverterStringParaContaCorrente(linha);

                var msg = $"Conta número {contaCorrente.Numero}, ag. {contaCorrente.Agencia}, Saldo {contaCorrente.Saldo}";
                Console.WriteLine(msg);
                //Console.WriteLine(linha);
            }
        }
        Console.ReadLine();
```

# Arquivos CSV

E se desejarmos colocar o sobrenome dos titulares, como `Jonatan Silva`.

Talvez utilizar o caractere de espaço como separador não seja uma boa escolha, pois podemos perder informações a respeito do nome do titular. No arquivo base `conta.txt`, trocarmos o caractere de espaço pela vírgula `,` de forma que o novo paradigma de organização dos conteúdos da conta seja: `375,4644,2483.13,Jonatan Silva`. Em nosso código, trocaremos o argumento de `Split()` para `','`.

```csharp
static ContaCorrente ConverterStringParaContaCorrente(string linha)
{
     string[] campos = linha.Split(',');

     var agencia = campos[0];
     var numero = campos[1];
     var saldo = campos[2].Replace('.', ' ,');
     var nomeTitular = campos[3];
}
```

O formato de arquivo que utilizamos é conhecido exatamente por "arquivo de valores separados por vírgula", em inglês teremos o acrônimo CSV - *Comma separated values*.

# Criando um arquivo CSV

O primeiro passo é estipular qual será o nome do novo arquivo que estamos criando. Para isso, faremos uma variável `caminhoNovoArquivo`.

```csharp
namespace ByteBankImportacaoExportacao
{
    partial class Program
    {

        static void CriarArquivo()
        {
            var caminhoNovoArquivo = "contasExportadas.csv";
        }

    }
}

```

Precisamos criar um fluxo de arquivo que nos permita colocar os bytes neste novo arquivo que criamos, logo, precisaremos de um stream de arquivo. Adicionaremos uma diretiva using ao código, seguida de uma variável `fluxoDeArquivo` que receberá `new FileStream`. Já criamos um  `FileStream`  anteriormente e o percurso que seguiremos será parecido. Adicionaremos como argumentos `caminhoNovoArquivo` e `FileMode`, mas dessa vez não usaremos o `.Open`, pois não queremos abrir um arquivo e sim criar, por isso neste caso usaremos `.Create`.

Como nós queremos escrever em um arquivo, usaremos o método  `Write()`  que exige como argumentos um array de bytes, um número inteiro offset e outro count.

O próximo passo é obter os bytes por meio da string, logo, escreveremos a variável `bytes` que guardará `encoding.GetBytes()`.

```csharp
namespace ByteBankImportacaoExportacao
{
    partial class Program
    {

        static void CriarArquivo()
        {
            var caminhoNovoArquivo = "contasExportadas.csv";

            using (var fluxoDeArquivo = new FileStream(caminhoNovoArquivo, FileMode.Create))
            {
                var contaComoString = "456,7895,4785.40, Gustavo Santos";
                var encoding = Encoding.UTF8;

                var bytes = encoding.GetBytes(contaComoString);

                fluxoDeArquivo.Write(bytes, 0, bytes.Lenght);
            }
        }

    }
}
```

O arquivo foi criado com as informações.

# **StreamWriter**

Para escrevermos o código que nos ajudará a não lidarmos diretamente com bytes no momento de criação do arquivo, precisaremos escrever um método separado.

Faremos um bloco using para `FileStream`, e em seguida usaremos a variável `caminhoNovoArquivo`, seguido pelo modo de criação `FileMode.Create`. Já conhecemos a classe `StreamReader`, contudo não queremos ler, mas sim, escrever. Portanto usaremos o tipo `StreamWriter`, uma classe existente no .NET. Guardaremos a classe em um variável `escritor`.

`StreamWriter` necessita como argumento `fluxoDeArquivo`.

```csharp
static void CriarArquivoComWriter();
 {
     var caminhoNovoArquivo = "contasExportadas.csv"

     using (var fluxoDeArquivo = new FileStream(caminhoNovoArquivo, FileMode.Create))
		 using (var escritor = new StreamWriter(fluxoDeArquivo))
     {
          escritor.Write("456,65465,456.0,Pedro");  
     }

}
```

Dessa forma o arquivo também é criado.

# Flush

Quando estamos escrevendo em um arquivo , o que acontece internamente entre a aplicação, sistema operacional e dispositivo externo? No momento em que escrevemos algo no HD, paramos a aplicação e é enviada uma mensagem para o sistema operacional escrever o número de bytes no HD. Esse tempo entre o envio da mensagem pela aplicação e o recebimento há uma latência muito alta de tempo.

O `ScreamWriter()` possui um buffer, no caso do `WriteLine()` não estamos de fato escrevendo em um arquivo, mas sim enviando a informação para o buffer. Enquanto não encerramos esse "buffer interno" do `StreamWriter()` a informação não será despejada no `FileStream`, dessa forma forma economizamos o tempo em que precisávamos sempre chamar o sistema operacional, mas se chamamos uma única vez uma grande quantidade de informação o processo será bem mais rápido.

Há caso que precisamos escrever um arquivo que esteja imediatamente no HD, como por exemplo um arquivo de log, e para isso precisamos chamar no `escritor` o método `Flush()`, que despejará o buffer para a Stream.

```csharp
static void TestaEscrita() 
{
     var caminhoArquivo = "teste.txt";
      using (var fluxoDeArquivo = new FileStream(caminhoArquivo, FileMode.Create))
      using (var escritor = new StreamWriter(fluxoDeArquivo))
      {
           for (int i = 0; i < 1000000000; i++)
           {
               escritor.WriteLine($"Linha {i}");
               escritor.Flush(); // Despeja o buffer para o Stream

               Console.WriteLine($"Linha {i} foi escrita no arquivo. Tecle enter p adicionar mais uma!");
               Console.ReadLine();
           }
       }
}
```

# Escrita binária

Criaremos a variável `escritor` que receberá o `BinaryWriter()`, que produz representações de maneira binária em nosso stream. Passaremos como argumento o `fs`.

```csharp
namespace ByteBankImportacaoExportacao
{ 
    partial class Program 
    { 

        static void EscritaBinaria()
        { 
            using (var fs = new FileStream("contaCorrente.txt", FileMode.Create))
            using (var escritor = BinaryWriter(fs))
            {
                escritor.Write(456);//número da Agência
                escritor.Write(546544);//número da conta
                escritor.Write(4000.50;)//Saldo
                escritor.Write("Gustavo Braga");
            }
            }

            }
    }
}
```

Em seguida, trabalharemos com a leitura. Criaremos a variável `leitor` que receberá `BinaryReader`, cujo argumento será `fs`.

```csharp
static void LeituraBinaria() 
{
     using (var fs = new FileStream("contaCorrente.txt", FileMode.Open))
     using(var leitor = new BinaryReader(fs))
     {
         var agencia = leitor.ReadInt32();
         var numeroConta = leitor.ReadInt32();
         var saldo = leitor.ReadDouble();
         var titular = leitor.ReadString(); 

         Console.WriteLine($"{agencia}/{numeroConta} {titular} {saldo}");
}
```

# Streams da Console

Quandos usamos o comando `Console.ReadLine()` percebemos que o console espera a entrada de dados do usuário para prosseguir. Se a console observa a entrada de dados, teremos uma Stream internamente. Trabalharemos com esta Stream e veremos o métodos que são os mesmos do `FileStream`.

Em vez de criar um `FileStream` usaremos o método `OpenStandardInput()` da classe `Console` que retorna um `Stream`, portanto é esta última opção que usaremos pare recuperar a Stream de entrada.

```csharp
namespace ByteBankImportacaoExportacao 
{ 
    partial class Program 
    {
        partial class Program
        {
            static void UsarStreamDeEntrada()
            {
                using(var fluxoDeEntrada = Console.OpenStandardInput())
                {
                    var buffer = new byte[1024]; // 1kb

                    var bytesLidos = fluxoDeEntrada.Read(buffer, 0, 1024);

                    Console.WriteLine($"Bytes lidos na console: {bytesLidos}");
                }
            }
    }
}
```

Iremos armazenar essas informações em um novo arquivo. Já fizemos essa tarefa antes: primeiramente precisaremos de um novo Stream, logo criaremos a variável `fs`- de `FileStream` -, e faremos como que `FileStream` grave as informações em um arquivo que chamaremos e  `entradaConsole.txt`.

```csharp
namespace ByteBankImportacaoExportacao 
{ 
    partial class Program 
    {
        partial class Program
        {
            static void UsarStreamDeEntrada()
            {
                using(var fluxoDeEntrada = Console.OpenStandardInput())
                using(var fs = new FileStream("entradaConsole.txt", FileMode.Create))
                {
                    var buffer = new byte[1024]; // 1kb

                    while(true)
                    {

                        var bytes lidos = fluxoDeEntrada.Read(buffer, 0, 1024);

                        fs.Write(buffer, 0, bytesLidos);

												fs.Flush(); //Despeja o buffer para o Stream!

                        Console.WriteLine($"Bytes lidos na console: {bytesLidos}");

                    }

                }
            }
    }
}
```

Sempre que trabalhamos com uma forma simples de obter a entrada do usuário, isto é, utilizando o método `Console.ReadLine()` que nos retorna uma string. No arquivo `Program.cs`, usaremo o `Console.ReadLine()` e guardaremos o resultado em uma variável `nome`. Em seguida, escreveremos na tela (`Console.WriteLine()`) a mensagem `Digite o seu nome:`.

Em seguida, escreveremos novamente `Console.WriteLine()` que receberá a variável `nome`.

```csharp
namespace ByteBankImportacaoExportacao 
{ 
    partial class Program 
    { 
        static void Main(string[] args) 
        {
            Console.WriteLine("Digite o seu nome:");
                        var nome = Console.ReadLine(): 

            Console.WriteLine("Aplicação finalizada. . .");

            Console.ReadLine();
        }
    }
}
```

# Classe File

Temos uma classe no .NET chamada `File`. Trata-se de uma classe estática que possui uma série de métodos estáticos que nos ajudam com as tarefas relacionadas aos arquivos. Se queremos, por exemplo, ler todas as linhas de um arquivo, usaremos a classe estática e o método `ReadAllFiles()`, que como argumento receberá o arquivo `contas.txt`, e por fim, guardaremos esses conteúdos em uma variável `linhas`. Em Seguida exibiremos a informação na tela por meio de `Console.WriteLine()`, que receberá como parâmetro `linhas.Length`. Para finalizar, travamos a aplicação utilizando `Console.ReadLine()`

```csharp
namespace ByteBankImportacaoExportacao 
{ 
    partial class Program 
    { 
        static void Main(string[] args) 
        {

                var linhas =File.ReadAllLines("contas.txt");
                Console.WriteLine(linhas.Length);

                foreach (var linha in linhas)
                {
                    Console.WriteLine(linha);
                }

            Console.ReadLine();
        }
    }
}
```

Existe um outro método no `File()` chamado `ReadAllText()`, que retornará uma string com todo o conteúdo do arquivo.

Temos na classe `File` outro método chamado `ReadAllBytes()`, que retornará um array de bytes.

```csharp
namespace ByteBankImportacaoExportacao 
{ 
    partial class Program 
    { 
        static void Main(string[] args) 
        {

                var bytesArquivo = File.ReadAllBytes("contas.txt");
                Console.WriteLine($"Arquivo contas.txt possui{ bytesArquivo.Lenght} bytes");

                var linhas =File.ReadAllLines("contas.txt");
                Console.WriteLine(linhas.Length);

                foreach (var linha in linhas)
                {
                    Console.WriteLine(linha);
                }
```

Conhecemos os métodos que retornam o conteúdo de um arquivo, mas na classe `File` temos métodos que criam formas de se escrever em um arquivo, tais como `WriteAllText()` , que recebe como primeiro argumento o nome de um arquivo, que será `escrevendoComAClasseFile.txt`. O segundo argumento é o conteúdo a ser escrito neste arquivo, como será algo sucinto, escreveremos na mesma linha : `Testando File.WriteAllText`.

```csharp
File.WriteAllText("escrevendoComAClasseFile.txt", "Testando File.WriteAllText");

Console.WriteLine("Arquivo escrevendoComAClasseFile criado!");
```
