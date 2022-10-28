# Anotações

# Argumentos opcionais

```csharp
public ListaDeContaCorrente(int capacidadeInicial = 5)
```

Quando o compilador encontra a instância `new ListaDeContaCorrente()`, verifica que não informamos um valor de argumento e que essa instância tem um argumento opcional; o próprio compilador irá preenchê-la com o valor padrão que estabelecemos - no caso, `5`.

# Argumentos nomeados

```csharp
public void MeuMetodo(string texto = "texto padrao", int numero = 5)
{ 

}
```

Mas e quando temos dois argumentos opcionais e queremos alterar apenas o segundo? Nesse caso, na chamada do método, podemos digitar `numero: 10`:

```csharp
lista.MeuMetodo(numero: 10);
```

# Argumentos params

Usando a palavra-chave `params`, você pode especificar um [parâmetro do método](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/method-parameters) que aceita um número variável de argumentos. O tipo de parâmetro deve ser uma matriz unidimensional.

```csharp
static int SomarVarios(params int[] numeros)
{ 
    int acumulador = 0;
    foreach(int numero in numeros)
    { 
        acumulador += numero;
    }
    return acumulador;
}
```

```csharp
public void AdicionarVarios(params ContaCorrente[] itens)
{
    foreach(ContaCorrente conta in itens) 
    {
        Adicionar(conta);
    }
}
```

Quando chamamos o método `AdicionarVarios()` com diversos objetos do tipo `ContaCorrente`, o compilador verifica que não temos uma sobrecarga que recebe, por exemplo, 8 argumentos.

Porém, temos um `AdicionarVarios()` que recebe um array marcado com `params`. Consequentemente, o compilador criará um array com todos os objetos que passamos para ele e irá chamar o nosso `lista.AdicionarVarios(contas)`, passando essa referência para o array.

# Métodos de extensão

No C# é possível criar métodos/funcionalidades à um tipo que já foi definido sem ter que criar um novo tipo derivado., fazendo com que essa funcionalidade se comporte como se fosse parte do tipo.

### Exemplo:

A lista do .Net não possui um método `AdicionarVarios` com argumento `params`. Porém, ainda podemos criar um método estático que aceite o argumento `params`. Para isso, crie a classe estática `ListExtensoes` e criar o método estático `AdicionarVarios` .

```csharp
public static class ListExtensoes
{
		public static void AdicionarVarios(List<int> listaDeInteiros, params int[] itens){
		    foreach(int item in itens)
		    {
		        listaDeInteiros.Add(item);
		    }
		}
}
```

Para termos um método de extensão de fato e poder usar como os métodos de instância que estamos acostumados, precisamos usar a sintaxe de método de extensão do C#.

Para isso, vamos voltar ao método `AdicionarVarios` e adicionar o modificador `this` no primeiro argumento:

```csharp
public static class ListExtensoes
{
		public static void AdicionarVarios(this List<int> listaDeInteiros, params int[] itens){
		    foreach(int item in itens)
		    {
		        listaDeInteiros.Add(item);
		    }
		}
}
```

Com esta modificação, podemos chamar o método a partir da classe estática, no modelo tradicional, ou como um método de instância:

```csharp
List<int> idades = new List<int>();

idades.AdicionarVarios(1, 5, 14, 25, 38, 61);
```

# as

```csharp
var outraConta = obj as ContaCorrente;
```

Convertendo um objeto com a palavra reservada `as` faz com que o retorno seja `null` em vez de lançar uma exceção.
