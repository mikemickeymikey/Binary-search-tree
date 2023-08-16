using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArbreBinariCerca
{
    public class ArbreBinariCerca<K, V> where K : IComparable
    {
        K clau;
        V valor;
        ArbreBinariCerca<K, V> fEsquerra;
        ArbreBinariCerca<K, V> fDreta;
        int count;

        public ArbreBinariCerca()
        {
            
        }

        public ArbreBinariCerca(K clau, V valor)
        {
            this.clau = clau;
            this.valor = valor;
        }

        #region Propietats
        public bool EsBuit
        {
            get { return Count == 0; }
        }

        public bool TeFEsquerra
        {
            get { return fEsquerra != null; }
        }

        public bool TeFDreta
        {
            get { return fDreta != null; }
        }

        public bool EsFulla
        {
            get { return fDreta == default && fEsquerra == default; }
        }

        public ArbreBinariCerca<K, V> FEsquerra { get => fEsquerra; set => fEsquerra = value; }
        

        public ArbreBinariCerca<K, V> FDreta { get => fDreta; set => fDreta = value; }

        public int Count { get; set; }
        #endregion

        #region Mètodes
        public void Afegeix(K clau, V valor)
        {
            if (EsBuit)
            {
                this.clau = clau;
                this.valor = valor;
            }
            else this.Afegeix(this, new ArbreBinariCerca<K, V>(clau, valor));
            Count++;
        }

        public void Afegeix(ArbreBinariCerca<K, V>  arrel, ArbreBinariCerca<K, V> nouNode)
        {
            if(arrel.clau.CompareTo(nouNode.clau) > 0)
            {
                if (!arrel.TeFEsquerra) arrel.fEsquerra = nouNode;
                else arrel.FEsquerra.Afegeix(arrel.FEsquerra, nouNode);
            }
            else if(arrel.clau.CompareTo(nouNode.clau) < 0)
            {
                if (!arrel.TeFDreta) arrel.fDreta = nouNode;
                else arrel.FDreta.Afegeix(arrel.FDreta, nouNode);
            }
        }

        public V ConsultaValor(K clau)
        {
            V valor = default;
            ArbreBinariCerca<K, V> abc = ConsultaNode(clau);
            if (abc != null) valor = abc.valor;
            return valor;
        }

        public ArbreBinariCerca<K, V> ConsultaNode(K clau)
        {
            ArbreBinariCerca<K, V> abc = this;
            while (abc != null)
            {
                if (abc.clau.CompareTo(clau) == 0) break;
                if(abc.clau.CompareTo(clau) > 0) abc = abc.FEsquerra;
                else abc = abc.FDreta;
            }
            return abc;
        }

        public bool Elimina(ArbreBinariCerca<K, V> abc)
        {
            bool eliminat = false;
            if(abc != null)
            {
                if(TeFEsquerra && TeFDreta)
                {
                    ArbreBinariCerca<K, V> seguent = TrobaSeguent(abc);
                    Elimina(seguent);
                    abc.valor = seguent.valor;
                    abc.clau = seguent.clau;
                }
                else
                {
                    ArbreBinariCerca<K, V> seguent;
                    if (!abc.TeFEsquerra) seguent = abc.FEsquerra;
                    else seguent = abc.FDreta;
                    abc = seguent;
                }
                eliminat = true;
            }
            return eliminat;
        }

        private ArbreBinariCerca<K, V> TrobaSeguent(ArbreBinariCerca<K, V> abc)
        {
            ArbreBinariCerca<K, V> seguent = abc.FEsquerra;
            while (seguent.TeFDreta) seguent = seguent.FDreta;
            return seguent;
        }

        public List<KeyValuePair<K, V>> PreOrdre()
        {
            List<KeyValuePair<K, V>> llista = new();
            return PreOrdre(this, llista);
        }

        private List<KeyValuePair<K, V>> PreOrdre(ArbreBinariCerca<K, V> abc, List<KeyValuePair<K, V>> llista)
        {
            if (abc != null)
            {
                KeyValuePair<K, V> parella = new(abc.clau, abc.valor);
                llista.Add(parella);
                if(abc.TeFEsquerra) PreOrdre(abc.FEsquerra, llista);
                if (abc.TeFDreta) PreOrdre(abc.FDreta, llista);
            }
            return llista;
        }

        public List<KeyValuePair<K, V>> PostOrdre()
        {
            List<KeyValuePair<K, V>> llista = new();
            return PostOrdre(this, llista);
        }

        private List<KeyValuePair<K, V>> PostOrdre(ArbreBinariCerca<K, V> abc, List<KeyValuePair<K, V>> llista)
        {
            if (abc != null)
            {
                if (abc.TeFEsquerra) PostOrdre(abc.FEsquerra, llista);
                if (abc.TeFDreta) PostOrdre(abc.FDreta, llista);
                KeyValuePair<K, V> parella = new(abc.clau, abc.valor);
                llista.Add(parella);
            }
            return llista;
        }

        public List<KeyValuePair<K, V>> InOrdre()
        {
            List<KeyValuePair<K, V>> llista = new();
            return PostOrdre(this, llista);
        }

        private List<KeyValuePair<K, V>> InOrdre(ArbreBinariCerca<K, V> abc, List<KeyValuePair<K, V>> llista)
        {
            if (abc != null)
            {
                InOrdre(abc.FEsquerra, llista);
                KeyValuePair<K, V> parella = new(abc.clau, abc.valor);
                llista.Add(parella);
                InOrdre(abc.FDreta, llista);
            }
            return llista;
        }
        #endregion
    }
}
