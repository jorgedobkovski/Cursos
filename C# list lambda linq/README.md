# List, lambda e linq

<aside>
💡 Já existe uma classe List<T> no .Net pronta para ser usada.

</aside>

# List<T>

Representa uma lista fortemente tipada de objetos que podem ser acessados por índice. Fornece métodos para pesquisar, classificar e manipular listas.

```csharp
List<int> idades = new List<int>();

idades.Add(1);
idades.Add(5);
idades.Add(14);
idades.Add(25);
idades.Add(38);
idades.Add(61);

idades.Remove(5);

for (int i = 0 < idades.Count; i++)
{
        Console.WriteLine(idades[i]);
}

Console.ReadLine();
```

```
1
14
25
38
61
```

Em [Anotações](https://www.notion.so/Anota-es-2dfd340204014756aee78bf9d8a30d28) há um método de extensão para adicionar vários na list.

## Método .Sort()

| Sort() | Classifica os elementos em todo o List<T> usando o comparador padrão. (Funciona para int e string, mas não para classes criadas) |
| --- | --- |
| Sort(Comparison<T>) | Classifica os elementos em todo o List<T> usando o Comparison<T> especificado. |
| Sort(Int32, Int32, IComparer<T>) | Classifica os elementos em um intervalo de elementos em List<T> usando o comparador especificado. |
| Sort(IComparer<T>) | Classifica os elementos em todo o List<T> usando o comparador especificado. |

### IComparable

Para que outros objetos possam implementar o método Sort, eles precisam implementar a interface `IComparable` que define apenas um método, o `CompareTo`.

No exemplo a interface será implementada na classe ContaCorrente:

```csharp
public class ContaCorrente : IComparable
{
		public int CompareTo(object obj)
		{				
		}
}
```

O Método CompareTo funciona da seguinte maneira:

- Retornar um número negativo quando sua instância *(`this`)* possuir precedência sobre o objeto `obj`;
- Retornar `0` quando a instância e `obj` forem equivalentes na ordenação;
- Retornar um número positivo diferente de `0` quando a precedência for do `obj`.

```csharp
public class ContaCorrente : IComparable
{
		public int CompareTo(object obj)
		{
				var outraConta = obj as ContaCorrente;	

				if (outraConta == null)
				{
				    return -1;
				}	

				if (Numero < outraConta.Numero)   //Ordenando por numero da conta
				{
				    return -1;
				}
				
				if (Numero == outraConta.Numero)
				{
				    return 0;
				}
				
				return 1;	
		}
}
```

> Mas a ordenação com a sobrecarga `Sort()` sempre usa a regra implementada no tipo da lista. E se, em um lugar da aplicação, quisermos mostrar as contas ordenadas pela propriedade `Saldo` e em outro lugar pela propriedade `Agencia`? Para isto precisamos criar uma classe especializada em comparações!
> 

### IComparer

Crie uma classe chamada `ComparadorContaCorrentePorAgencia` e implemente a interface `IComparer<ContaCorrente>`:

```csharp
public class ComparadorContaCorrentePorAgencia : IComparer<ContaCorrente>
{
    public int Compare(ContaCorrente x, ContaCorrente y)
    {

    }
}
```

No método `Compare`, vamos implementar a lógica de comparação do número de agência:

```csharp
public class ComparadorContaCorrentePorAgencia : IComparer<ContaCorrente>
{
    public int Compare(ContaCorrente x, ContaCorrente y)
    {
				if (x == y)
				{
				    return 0;
				}
				
				if(x == null)
				{
				    return 1;
				}
				
				// if (y == null)
				// {
				//     return -1;
				// }
		
				// if (x.Agencia < y.Agencia)
				// {
				//     return -1; // X fica na frente de Y
				// }
				
				// if (x.Agencia == y.Agencia)
				// {
				//     return 0; // São equivalentes
				// }
				
				// return 1;

				// o código acima é equivalente, uma vez que o tipo int implementa 
				// a interface IComparer

				return x.Agencia.CompareTo(y.Agencia);

		 }
}
```

# OrderBy [LINQ]

Classifica os elementos de uma sequência em ordem crescente. Retorna um objeto que implementa a interface `IOrderedEnumerable<TSource>`. 

```csharp
IOrderedEnumerable<ContaCorrente> contasOrdenadas = 
    contas.OrderBy(conta => conta.Numero);

foreach (var conta in contasOrdenadas)
{
        Console.WriteLine($"Conta número {conta.Numero}, ag. {conta.Agencia}");
}
```

# Mais expressões Lambda

Se colocarmos valores Null na nossa lista de contas veremos que o OrderBy não conseguirá lidar com eles e lançará uma exceção.

Por isso precisaremos criar um If para verificar os valores null, porém, a expressão `conta => conta.Numero` é muito simples. Ela tem uma flecha (`=>`) e a expressão de valor. Inclusive, essa expressão possui um nome bastante forte, é uma definição matemática que foi utilizada em C#, trata-se de uma **expressão lambda**.

> Quero mostrar que essa expressão é equivalente ao que está à esquerda do operador. Então, para cada `conta`, retornaremos `Numero`. Sendo assim, poderíamos escrever da seguinte forma:
> 

```csharp
IOrderedEnumerable<ContaCorrente> contasOrdenadas = 
    contas.OrderBy(conta => { return conta.Numero } );
```

Dessa forma podemos colocar uma lógica para o retorno dos números. Como queremos que os valores Null fiquem no final da lista, retornamos  `int.MaxValue` que representa o maior valor de um Int32, fazendo que os itens nulos sejam ordenados no fim da lista.

```csharp
IOrderedEnumerable<ContaCorrente> contasOrdenadas =
        contas.OrderBy(conta => {
                if(conta == null)
                {
                        return int.MaxValue;
                }

                return conta.Numero;
        });
```

# Where [LINQ]

Podemos filtrar os itens de uma coleção com uso do método `Where`. O `Where` recebe como argumento uma expressão lambda que retorna um valor booleano: `true` ou `false`:

```csharp
var contasNaoNulas = contas.Where(conta => conta != null);
```

Note que o método `Where` retorna o tipo `IEnumerable<T>` - o mesmo tipo usado pelo método de extensão `OrderBy`. Então podemos encadear nossas chamadas:

```csharp
var contasOrdenadas = contas
    .Where(conta => conta != null)
    .OrderBy(conta => conta.Numero);
```

Quando falamos de `Where()` e `OrderBy()`, estamos falando de `Linq`, nome da tecnologia da biblioteca criada pela Microsoft, para utilizarmos essas ordenações, por meio de métodos de extensão.
