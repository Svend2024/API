using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class TransactionHistoriesController : BaseEntitiesController<TransactionHistory>
    {
        public TransactionHistoriesController(IBaseService<TransactionHistory> context) : base(context) { }
    }
}
