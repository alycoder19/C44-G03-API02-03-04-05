using ECommerce.Service.Abstraction;
using ECommerce.Shared.DTOS.BasketDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        #region Get Basket By Id

        [HttpGet]

        public async Task<ActionResult<BasketDto>>GetBasket(string id)
        {

            var Basket = await _basketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        #endregion

        #region Creat or  Update Basket

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreatOrUpdateBasket(BasketDto basket)
        {
            var Basket=await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);

        }

        #endregion

        #region Delete Basket By Id

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>DeleteBasket(string id)
        {
            var Result=await _basketService.DeleteBasketAsync(id);
            return Ok(Result);

        }




        #endregion

    }
}
