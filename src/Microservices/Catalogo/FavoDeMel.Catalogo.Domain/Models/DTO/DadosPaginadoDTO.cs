using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Domain.Models.DTO
{
    public class DadosPaginadoDTO<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; private set; }

        public DadosPaginadoDTO(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
