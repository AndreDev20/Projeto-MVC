using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendedores
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Vendedores vendedores = new Vendedores(10); // limite de 10 vendedores
            int opcao;

            do
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                    opcao = -1;

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o programa...");
                        break;

                    case 1:
                        Console.Write("ID do vendedor: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nome do vendedor: ");
                        string nome = Console.ReadLine();
                        Console.Write("Percentual de comissão (%): ");
                        double perc = double.Parse(Console.ReadLine());

                        Vendedor v = new Vendedor(id, nome, perc);

                        if (vendedores.AddVendedor(v))
                            Console.WriteLine("Vendedor cadastrado com sucesso!");
                        else
                            Console.WriteLine("Limite de vendedores atingido (máximo 10).");
                        break;

                    case 2:
                        Console.Write("Informe o ID do vendedor para consulta: ");
                        int idConsulta = int.Parse(Console.ReadLine());
                        Vendedor vConsulta = null;

                        // buscar vendedor por ID
                        for (int i = 0; i < vendedores.Qtde; i++)
                        {
                            if (vendedores.SearchVendedor(vendedores[i])?.Id == idConsulta)
                            {
                                vConsulta = vendedores.SearchVendedor(vendedores[i]);
                                break;
                            }
                        }

                        if (vConsulta != null)
                        {
                            double totalVendas = vConsulta.ValorVendas();
                            double comissao = totalVendas * vConsulta.PercComissao / 100.0;

                            Console.WriteLine($"\nID: {vConsulta.Id}");
                            Console.WriteLine($"Nome: {vConsulta.Nome}");
                            Console.WriteLine($"Total de Vendas: {totalVendas:C2}");
                            Console.WriteLine($"Comissão Devida: {comissao:C2}");

                            // média de vendas por dia registrado
                            double somaMedias = 0;
                            int diasComVenda = 0;
                            for (int i = 1; i <= 31; i++)
                            {
                                var venda = vConsulta.GetVenda(i);
                                if (venda != null)
                                {
                                    somaMedias += venda.ValorMedio();
                                    diasComVenda++;
                                }
                            }
                            double mediaDiaria = diasComVenda > 0 ? somaMedias / diasComVenda : 0;
                            Console.WriteLine($"Valor Médio das Vendas Diárias: {mediaDiaria:C2}");
                        }
                        else
                        {
                            Console.WriteLine("Vendedor não encontrado.");
                        }
                        break;

                    case 3:
                        Console.Write("Informe o ID do vendedor para exclusão: ");
                        int idExcluir = int.Parse(Console.ReadLine());
                        Vendedor vExcluir = null;

                        // procurar por ID
                        for (int i = 0; i < vendedores.Qtde; i++)
                        {
                            if (vendedores[i].Id == idExcluir)
                            {
                                vExcluir = vendedores[i];
                                break;
                            }
                        }

                        if (vExcluir != null)
                        {
                            if (vExcluir.ValorVendas() == 0)
                            {
                                vendedores.DelVendedor(vExcluir);
                                Console.WriteLine("Vendedor excluído com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Não é possível excluir. Já existem vendas registradas.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vendedor não encontrado.");
                        }
                        break;

                    case 4:
                        Console.Write("Informe o ID do vendedor: ");
                        int idVenda = int.Parse(Console.ReadLine());
                        Vendedor vVenda = null;

                        for (int i = 0; i < vendedores.Qtde; i++)
                        {
                            if (vendedores[i].Id == idVenda)
                            {
                                vVenda = vendedores[i];
                                break;
                            }
                        }

                        if (vVenda != null)
                        {
                            Console.Write("Dia da venda (1-31): ");
                            int dia = int.Parse(Console.ReadLine());
                            Console.Write("Quantidade vendida: ");
                            int qtde = int.Parse(Console.ReadLine());
                            Console.Write("Valor total da venda: ");
                            double valor = double.Parse(Console.ReadLine());

                            Venda novaVenda = new Venda(qtde, valor);
                            vVenda.RegistrarVenda(dia, novaVenda);

                            Console.WriteLine("Venda registrada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Vendedor não encontrado.");
                        }
                        break;

                    case 5:
                        double totalGeralVendas = 0;
                        double totalGeralComissao = 0;

                        Console.WriteLine("\n=== LISTA DE VENDEDORES ===");
                        for (int i = 0; i < vendedores.Qtde; i++)
                        {
                            Vendedor vList = vendedores[i];
                            double total = vList.ValorVendas();
                            double comissao = total * vList.PercComissao / 100.0;

                            Console.WriteLine($"ID: {vList.Id}, Nome: {vList.Nome}, Vendas: {total:C2}, Comissão: {comissao:C2}");

                            totalGeralVendas += total;
                            totalGeralComissao += comissao;
                        }

                        Console.WriteLine("\n--- Totais ---");
                        Console.WriteLine($"Total de Vendas: {totalGeralVendas:C2}");
                        Console.WriteLine($"Total de Comissões: {totalGeralComissao:C2}");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);

        }
    }
}
