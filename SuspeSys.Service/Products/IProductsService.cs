using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Products
{
    public interface IProductsService
    {
        int GetCurrentProductNumber(string groupNo);
        long GetCurrentProductNumber();
       bool  AddProducts(IList<DaoModel.Products> productsList, string mainTrackNumber = null,int currentProductNumber=0);
        void ProductsHangingPiece(string productsId);
        /// <summary>
        /// 【协议2.0】制品界面发送消息到挂片站
        /// </summary>
        /// <param name="productsList">产品列表</param>
        /// <param name="mainTrackNo">主轨号(10进制)</param>
        /// <param name="statingNo">挂片站号(10进制)</param>
        /// <param name="productNumber">排产号(10进制)</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        //bool BindProudctsToHangingPiece(List<byte> productsList, string mainTrackNo, string statingNo, int productNumber, ref int address, bool isEnd, ref string errMsg);
        bool BindProudctsToHangingPiece(List<byte> productsList, string mainTrackNo, string statingNo, int productNumber, ref string errMsg);

        bool ProductsOnline(DaoModel.Products p);
        bool ClientMancheOnLine(DaoModel.Products p, int mainTrackNumber, ref string errMsg);
        bool ClearHangingPiece(string productId);
        bool AllocationHangerPiece(string productId, string hangingPieceNo);
        bool MarkSuccessProducts(DaoModel.Products row, ref string errMsg);
    }
}
