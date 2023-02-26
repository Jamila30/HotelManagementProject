using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Stripe
{
	public record CreateChargeResource(
	string Currency,
	long Amount,
	string CustomerId,
	string ReceiptEmail,
	string Description);
}
