# Bibliotecas DLL, documentação e NuGet

> Para tornar nosso código de fácil manutenção e evitar a duplicação de código será da mesma forma que usamos tipo do .NET. Quando usamos o tipo `int` e `string` percebemos que eles já foram definido em algum lugar. **Estamos usando apenas a biblioteca .NET**, que define `string` e `int`- assim como tem definido `double` e `Exception`. Seria interessante se tivéssemos a mesma habilidade de encapsular as classes comuns em uma biblioteca e usá-la em diversos projetos que dependem do mesmo recursos.
> 

Adicione um novo projeto à aplicação, sendo ele uma Biblioteca de classes.

![image](https://user-images.githubusercontent.com/47338154/192934108-0e0f3e3f-0d51-45f3-b629-793b040499c7.png)

É necessário fazer os projetos se enxergarem dentro da solução.

Adicionar referências:

![image](https://user-images.githubusercontent.com/47338154/192934143-b38503ee-2605-4b30-aede-1322c6f5dd39.png)

Depois fazer o uso do namespace da biblioteca de classes no projeto aplicação.

```csharp
using ByteBank.Modelos;
```

No arquivo *.csproj* é possível ver as referências entre os projetos da solução: 

```html
<ItemGroup> 
    <ProjectReference Include="..\ByteBank.Modelos\ByteBank.Modelos.csproj">
        <Project>{c2175f0d-7024-465e-947f-c0fb51887a9c}</Project>
        <Name>ByteBank.Modelos</Name>
    </ProjectReference>
</ItemGroup>
```

Além disso também é adicionado na pasta do .exe [bin > Debug] o arquivo de biblioteca .dll 

![image](https://user-images.githubusercontent.com/47338154/192934178-261e79dc-f043-4397-9164-b759f5c4bde2.png)

É possível criar classes visíveis apenas dentro de um projeto da solução através do modificador de acesso `internal`. Dessa forma garantimos que apenas as classes desse projeto possam utilizar esses recursos e assim, caso seja necessário fazer alguma modificação, outros projetos não quebrem.

Se quisermos criar uma classe que ajude a parte de autenticação dos projetos da solução podemos criar uma classe Helper. Por exemplo podemos encapsular a lógica de comparar uma senha com outra, colocando o código em um classe ajudante.

```csharp
namespace ByteBank.Modelos
{
    internal class AutenticacacoHelper
    {
        public bool CompararSenhas(string senhaVerdadeira, string senhaTentativa)
        {
            return senhaVerdadeira == senhaTentativa;
        }
    }
}
```

Criamos `AutenticacaoHelper`, e agora, podemos usá-la no `ParceiroComercial`. Vamos adicionar `_autenticacaoHelper`, que será igual a `new AutenticacaoHelper()`.

```csharp
namespace ByteBank.Modelos
{
    public class ParceiroComercial : IAutenticavel
    {
        private AutenticacaoHelper _autenticacaoHelper = new AutenticacaoHelper();
        public string Senha { get; set; }

        public FuncionarioAutenticavel(double salario, string cpf)
            : base(salario, cpf)
        {
        }

        public bool Autenticar(string senha)
        {
            return _autenticacaoHelper.CompararSenhas(Senha, senha);
        }
    }
}
```

É possível também usar o modificador internal protected, que vai fazer com que a classe ou método em questão seja visível apenas ao projeto em que está inserido e nas classes de fora do projeto que derivam dessa classe.

```csharp
public abstract void AumentarSalario();

    internal protected abstract double GetBonificacao();
```

Desta forma, `GetBonificacao` será visível dentro do projeto `Modelos`e para qualquer classe que derive, inclusive fora do projeto. Vamos fazer um teste, `Estagio.cs` se enquadra no caso fora de projeto. Para respeitar a coerência entre a visibilidade definida na classe base e na derivada, precisaremos repetir a alteração e trocar `internal` por `protected.`

```csharp
public class Estagiario : Funcionario
{

    public override void AumentarSalario()
    {
        //Qualquer código
    }
    protected override double GetBonificacao()
    {
        throw new NotImplementedException();
    }
}
```

É possível usar a biblioteca que usamos em outros projetos de outras soluções.

Para isso é importante criar um repositório para armazenar as versões estáveis dos arquivos `.dll` das bibliotecas criadas. 

> Atenção: Não referenciar um arquivo `.dll` dentro do diretório bin, dentro da pasta Debug de um projetos pois trata-se de um arquivo temporário, que sempre altera, conforme o VS recompila o código.
> 

Criando um diretórios para essas bibliotecas é só referenciá-lo no projeto.

![image](https://user-images.githubusercontent.com/47338154/192934230-0c738a68-b194-4348-b749-0bddbc79fb7c.png)

E então adicionar o namespace da biblioteca normalmente no projeto para usá-la.

```csharp
using ByteBank.Modelos;
```

# Documentando uma biblioteca

Numa mesma solução é possível criar a documentação do projeto biblioteca de classes através dos comentários com três barras `///` e com as tags XML de documentação do .NET que são feitos acima do código a ser documentado.

Veja no exemplo abaixo como fazer a criação desse comentário que é interpretado pelo compilador:

```xml
///<summary>
///Cria uma instância de ContaCorrente com os argumentos utilizados.
/// <summary>
///<param name="agencia">Representa o valor da proprieddae <see cref="" /> e deve possuir um valor maior que zero.</param>
///<param name="numero">Representa o valor da proprieddae Numero e deve possuir um valor maior que zero.</param>
```

```csharp
public ContaCorrente(int agencia, int numero)
{
    if (numero <= 0)
    {
        throw new ArgumentException("O argumento agencia deve ser maior que 0.", nameof(agencia));
    }

    if (numero <= 0)
    {
        throw new ArgumentException("O argumento numero deve ser maior que 0.", nameof(numero));
    }

    NumeroAgencia = agencia;
    Numero = numero;

    TotalDeContasCriadas++;
    TaxaOperacao = 30 / TotalDeContasCriadas;
}
```

No entanto as bibliotecas de arquivos `DLL` devem ser documentadas de outra forma.

> Por mais que tenha sido usado um comentário diferente para a escrita da documentação, `//`
sempre sinaliza um comentário, que é ignorado pelo compilador. Isto significa que esse trecho não é gerado, porque está fora da `DLL`.
> 

## Documentando uma biblioteca em .DLL

Para gerar uma documentação .xml é necessário alterar a build do projeto da biblioteca de classes. 

> No "Gerenciador de Soluções", clicaremos com o mouse em `ByteBank.Modelos`e selecionaremos a opção "Propriedades". Depois, no painel que será aberto, clicaremos em "Build", encontrando diversas opções. Dentre elas, nosso interesse está na seção "Saída". Nela, vamos marcar o *check box* "Arquivo de documentação XML", o Visual Studio vai auto completar um caminho padrão para o arquivo da documentação  `bin\Debug\ByteBank.Modelos.xml`.
> 

![image](https://user-images.githubusercontent.com/47338154/192934305-3cad0bb0-33e8-4514-b46e-f38f7889aa6f.png)

Depois iremos recompilar a solução para que o Visual Studio gere o arquivo de documentação .xml no diretório. Agora é só copiar o código e colar no mesmo diretório de bibliotecas .dll estáveis que criamos anteriormente.

Podem surgir alguns warnings nos trechos de códigos da biblioteca que são visíveis externamente, pois agora se faz necessário adicionar um documentação a eles.

Também é possível adicionar na documentação uma referencia as exceptions  do trecho de código, como no exemplo abaixo.

```xml
/// <summary>
/// Realiza o saque e atualiza o valor da propriedade <see cref="Saldo"/>
/// <exception cref="ArgumentException">Execção lançada quando o valor de <paramref name="valor"/>é maior que o valor da propriedade <see cref="Saldo">. </exception>
/// <exception cref="SaldoInsufienteException">Exceção lançada quando um valor negativo é utilizado no argumento <paramref name="valor"/>. </exception>
/// </summary>
/// <param name="valor"> Representa o valor do saque, deve ser maior que 0 e menor que <see cref="Saldo"/>. </param>
```

```csharp
public vod Sacar(double valor)
 {
    if(valor <  0)
    {
         throw new ArgumentException("Valor inválido para o saque.", nameof(valor));
    {

    if (_saldo < valor)
    {
        ContadorSaquesNaoPermitidos++;
        throw new SaldoInsuficienteException(Saldo, valor);
    }

    _saldo -= valor;
}
```

# Utilizando bibliotecas de pacotes NuGet

O NuGet é o gerenciador de pacotes do VS, que pode ser usado para aplicações .NET. 

No site do NuGet encontraremos várias bibliotecas que podem ser aproveitados pelo editor, após aplicarmos uma configuração.

Encontraremos facilmente o link para baixar o `Humanizer.`

![image](https://user-images.githubusercontent.com/47338154/192934340-a2b40324-9220-4b27-8547-3f824e4d8a39.png)

```powershell
Package Manager> Install-Package Humanizer -Version 2.3.3
```

No menu, selecionaremos "Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciados de Pacotes". Em seguida, será aberta a console do Gerenciador de Pacotes. 

Lembre-se de selecionar o pacote correto para instalar o pacote.

![image](https://user-images.githubusercontent.com/47338154/192934384-ec232cac-63fb-4adf-97f5-069845262e61.png)

Dessa forma o VS cria um repositório `package` no diretório da solução com os arquivos da biblioteca e adiciona o arquivo `packages.config`  no diretório do projeto, é um arquivo XML que define todas as dependências, pacotes, instalados do NuGet.

![repositório `package` no diretório da solução.](https://user-images.githubusercontent.com/47338154/192934467-ce2a80f1-9d3b-4b7f-af7a-f8ca109e9846.png)

![o arquivo `packages.config`  no diretório do projeto](https://user-images.githubusercontent.com/47338154/192934543-2fa8bd52-49ed-4ff2-a8a4-c63ff9eddfa5.png)

> Agora, este arquivo que descreve as dependências do projeto, passou a integrá-lo também. Inclusive, o diretório `packages` é desnecessário. Se deletarmos essa pasta, com todas as listas de DLL que o VS fez o download; ao acessarmos o editor e executarmos o código, tudo acontecerá conforme o esperado: no console, visualizaremos a mensagem `Vencimento em 40 minutos`.
> 

Clicando com botão direito na "Gerenciador de Soluções" existe a opção  "Restaurar Pacotes NuGet", que faz a restauração dos pacotes, acessa o servidor do NuGet e faz o download dos arquivos DLL.
