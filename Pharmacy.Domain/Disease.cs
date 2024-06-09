using Pharmacy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Entities
{
	public class Disease : Entity, IAggregateRoot
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}


