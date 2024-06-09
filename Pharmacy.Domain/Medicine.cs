using Pharmacy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Entities
{
	public class Medicine : Entity, IAggregateRoot
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
