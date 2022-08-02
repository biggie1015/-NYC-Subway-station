using SubwayEntrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayEntrance.Data.EFCore
{
    public class EFUserWithSubwayRepository : EFCoreRepository<UserWithSubway, SubwayContext>
    {
        public EFUserWithSubwayRepository(SubwayContext subwayContext) : base(subwayContext)
        {

        }
    }
}
