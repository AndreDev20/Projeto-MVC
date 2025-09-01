using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendedores
{
    internal class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public int Max { get => max; }
        public int Qtde { get => qtde; }

        public Vendedores(int max)
        {
            this.max = max;
            osVendedores = new Vendedor[max];
            qtde = 0;
        }

        public Vendedor this[int index]
        {
            get
            {
                if (index >= 0 && index < qtde)
                    return osVendedores[index];
                return null;
            }
        }

        public bool AddVendedor(Vendedor v)
        {
            if(Qtde < Max)
            {
                osVendedores[qtde] = v;
                qtde++;
                return true;
            }

            return false;
        }

        public bool DelVendedor(Vendedor v)
        {
            for (int i = 0; i < Qtde; i++)
            {
                if (osVendedores[i] == v)
                {
                    for (int j = i; j < Qtde - 1; j++)
                    {
                        osVendedores[j] = osVendedores[j + 1];
                    }
                    qtde--;
                    osVendedores[qtde] = null;
                    return true;
                }
            }
            return false;
        }

        public Vendedor SearchVendedor(Vendedor v)
        {
            for (int i = 0; i < osVendedores.Length; i++)
            {
                if (osVendedores[i] == v)
                {
                    return osVendedores[i];
                }
            }

            return null;
        }

        public double ValorVendas()
        {
            double total = 0;

            for (int i = 0; i < qtde && i < osVendedores.Length; i++)
            {
                total += osVendedores[i].ValorVendas();
            }

            return total;
        }

        public double ValorComissao()
        {
            double total = 0;

            for (int i = 0; i < Qtde && i < osVendedores.Length; i++)
            {
                // Corrigido: Vendedor não possui método ValorComissao, então calculamos aqui
                total += osVendedores[i].ValorVendas() * osVendedores[i].PercComissao / 100.0;
            }

            return total;
        }

    }
}
