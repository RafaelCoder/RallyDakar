using RallyDakar.Dominio.Entidades;
using System.Collections.Generic;

namespace RallyDakar.Dominio.Interfaces {
    public interface IPilotoRepositorio {
        void Adicionar(Piloto piloto);
        IEnumerable<Piloto> ObterTodos();
        IEnumerable<Piloto> ObterTodosPorNome(string nome);
        Piloto Obter(int pilotoId);
        bool Existe(int pilotoId);
        //bool Existe(Piloto piloto);
    }
}
