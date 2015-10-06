using System.Collections.Generic;
using System.Linq;

namespace Gorilla.Utilities.Web.Mvc.Models
{
    public class DataTableResponse<T>
    {

        public DataTableResponse(int requestDraw, IEnumerable<T> data = null)
        {
            this.draw = requestDraw;

            if (data == null)
                return;

            this.data = data;
            this.recordsTotal = data.Count();
        }

        /// <summary>
        /// Usado para segurança, setar com o mesmo dado do request
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Numero total de dados para paginação
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// Numero de dados atuais
        /// </summary>
        public int recordsFiltered
        {
            get { return this.recordsTotal; }
        }

        /// <summary>
        /// List/Array com os dados
        /// </summary>
        public IEnumerable<T> data { get; set; }

    }
}