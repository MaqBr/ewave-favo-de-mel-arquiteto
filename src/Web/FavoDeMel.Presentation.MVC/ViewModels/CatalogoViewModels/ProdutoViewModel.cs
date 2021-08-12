using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Profundidade { get; set; }
        public IEnumerable<CategoriaViewModel> Categorias { get; set; }
    }

}