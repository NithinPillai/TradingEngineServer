﻿using System;
namespace TradingEngineServer.Orders
{
	public class Order : IOrderCore
	{
		private readonly IOrderCore _orderCore;

		public Order(IOrderCore orderCore, long price, uint quantity, bool isBuySide)
		{
			Price = price;
			InitialQuantity = quantity;
			CurrentQuantity = quantity;
            IsBuySide = isBuySide;

			_orderCore = orderCore;
		}

		public Order(ModifyOrder modifyOrder) : this(modifyOrder, modifyOrder.ModifyPrice, modifyOrder.ModifyQuantity, modifyOrder.ModifyIsBuySide)
		{}

		public long Price { get; private set; }
		public uint InitialQuantity { get; private set; }
		public uint CurrentQuantity { get; private set; }
		public bool IsBuySide { get; private set; }

		public long OrderId => _orderCore.OrderId;

		public string Username => _orderCore.Username;

		public int SecurityId => _orderCore.SecurityId;

        public void IncreaseQuantity(uint quantityDelta)
		{
			CurrentQuantity += quantityDelta;
		}

        public void DecreaseQuantity(uint quantityDelta)
        {
			if (quantityDelta > CurrentQuantity)
			{
				throw new InvalidOperationException($"Quantity Delta > Current Quantity for OrderId={OrderId}");
			}
            CurrentQuantity -= quantityDelta;
        }
    }
}

