using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.WebApi;
using MyBeerTap.Model;

namespace MyBeerTap.ApiServices
{
   public interface IPourBeerApiService: ICreateAResourceAsync<PourBeer, int>
    {

    }
}
