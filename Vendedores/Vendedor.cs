using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendedores
{
    internal class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas = new Venda[31];

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public double PercComissao { get => percComissao; set => percComissao = value; }

        public Vendedor(int id, string nome, double percComissao)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
        }

        public void RegistrarVenda(int dia, Venda venda)
        {
            if(dia >= 1 && dia <= 31)
            {
                asVendas[dia - 1] = venda;
            }
        }

        public double ValorVendas()
        {
            double total = 0;

            foreach (Venda venda in asVendas)
            {
                if(venda != null)
                {
                    total += venda.Valor;
                }
            }

            return total;
        }

        public Venda GetVenda(int dia)
        {
            if (dia >= 1 && dia <= 31)
                return asVendas[dia - 1];
            return null;
        }


    }
}
