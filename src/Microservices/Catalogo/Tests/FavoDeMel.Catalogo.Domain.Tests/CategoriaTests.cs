using FavoDeMel.Catalogo.Domain;
using FavoDeMel.Domain.Core.DomainObjects;
using Xunit;

namespace FavoDeMel.Catalogo.Unit.Tests
{
    public class CategoriaTests
    {
        [Fact]
        [Trait("Categoria", "Unidade - Validar Entidade Categoria")]
        public void Categoria_Validar_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Categoria(string.Empty, 102)
            );

            Assert.Equal("O campo Nome da categoria não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Categoria("Nome", 0)
            );

            Assert.Equal("O campo Codigo não pode ser 0", ex.Message);
        }
    }
}
