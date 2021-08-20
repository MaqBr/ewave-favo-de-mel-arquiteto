using FavoDeMel.Domain.Core.DomainObjects;
using Xunit;
using FavoDeMel.Venda.Domain.Models;
using System;

namespace FavoDeMel.Catalogo.Unit.Tests
{
    public class ComandaTests
    {
        [Fact]
        [Trait("Comanda", "Unidade - Validar Entidade Comanda")]
        public void Comanda_Validar_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Comanda(Guid.NewGuid(), string.Empty, false, 0, 0)
            );

            Assert.Equal("O campo Codigo da comanda não pode estar vazio", ex.Message);
        }
    }
}
