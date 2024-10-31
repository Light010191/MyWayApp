using Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Products
{
	public  class GeneralDbResponses
	{
		public static ServiceResponse ItemAlreadyExist(string itemName) => new(true, $"{itemName} уже создан");
		public static ServiceResponse ItemNotFound(string itemName) => new(true, $"{itemName} не найден");
		public static ServiceResponse ItemCreated(string itemName) => new(true, $"{itemName} успешно создан");
		public static ServiceResponse ItemUpdate(string itemName) => new(true, $"{itemName} успешно обновлен");
		public static ServiceResponse ItemDelete(string itemName) => new(true, $"{itemName} успешно удален");

	}
}
