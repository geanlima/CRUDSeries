using System;

namespace Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;   
                    case "3":
                        AtualizarSerie();
                        break;      
                    case "4":
                        ExcluirSerie();
                        break; 
                    case "5":
                        VisualizarSerie();
                        break;         
                    case "C":
                        Console.Clear();
                        break;    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                opcaoUsuario = ObterOpcaoUsario();
            }
            Console.WriteLine("Obrigado");
            Console.ReadLine();
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da serie: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornoPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da serie: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }
        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o id da serie: ");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Serie serie = Auxiliar("Atualizar");

            repositorio.Atualiza(serie.Id, serie);
        }
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova series");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Serie serie = Auxiliar();

            repositorio.Insere(serie);
        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar series");
            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma serie cadastrada");
                return;
            }
            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido() ? " - Excluido" : "";

                Console.WriteLine($"#ID: {serie.retornoId()} - {serie.retornaTitulo()} {excluido}");
            }

        }
        private static string ObterOpcaoUsario()
        {
            Console.WriteLine();
            Console.WriteLine("Series a seu dispor!!!");
            Console.WriteLine("Informe a opcao desejada:");

            Console.WriteLine("1- Lista series");
            Console.WriteLine("2- Inserir nova serie");
            Console.WriteLine("3- Atualizar serie");
            Console.WriteLine("4- Excluir serie");
            Console.WriteLine("5- Vizualizar serie");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine("");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }

        public static Serie Auxiliar(string metodo = null)
        {
            int indiceId;
            if(metodo == "Atualizar")
            {
                Console.Write("Digite o id da Serie: ");
                indiceId = int.Parse(Console.ReadLine());
            } 
            else
            {
                indiceId = repositorio.ProximoId();
            }

            Console.Write("Digite o genero entre as opcoes acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Digite o titulo da seire: ");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Digite o Ano de Inicio da Serie : ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Digite a descricao da Serie: ");
            string entradaDescricao = Console.ReadLine();
            Serie serie = new Serie(id: indiceId, 
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);
            return serie;

        }
    }
}
