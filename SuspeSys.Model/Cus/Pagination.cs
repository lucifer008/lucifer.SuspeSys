using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class Pagination
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 列
        /// </summary>
        public string Colomns { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Tables { get; set; }

        /// <summary>
        /// 排序 必填
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WhereStr { get; set; }

        private int _pageBegin { get { return (this.PageNumber - 1) * this.PageSize; } }

        private int _pageEnd { get { return this.PageNumber * PageSize; } }

        /// <summary>
        /// 查询语句
        /// </summary>
        public string QueryString
        {
            get
            {

                return $@"
                            WITH T1 AS
                            (
                                SELECT {Colomns} ,ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS ROWNUMBER FROM {Tables}
                                WHERE 1=1  {  _where }
                            )
                            SELECT * FROM T1 WHERE ROWNUMBER > {_pageBegin} AND  ROWNUMBER <= {_pageEnd}

                    ";
            }
        }

        private string _where
        {
            get
            {
                return string.IsNullOrEmpty(WhereStr) ? "" : $"  { this.WhereStr}";
            }
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        public string QueryCount
        {
            get
            {
                return $"SELECT count(1) FROM { Tables}  WHERE 1=1  {  _where }";
            }
        }
    }

    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationResult<T>
    {
        public int Total { get; set; }

        public IList<T> Data { get; set; }
    }
}
