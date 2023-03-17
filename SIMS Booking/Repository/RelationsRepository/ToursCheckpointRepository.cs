using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ToursCheckpointRepository : RelationsRepository<ToursCheckpoint>
    {
        public ToursCheckpointRepository() : base("../../../Resources/Data/toursCheckpoint.csv") { }


        public void LoadToursCheckpoint( TourRepository t ,TourPointRepository tp )
        {

            foreach(ToursCheckpoint tc in _entityList)
            {

                foreach(Tour tour in t.GetAll() )
                {


                    if(tc.TourId == tour.getID() )
                    {

                        tour.tourPoints.Add(tp.GetById(tc.CheckpointId));
                    }
                }

            }

        }


    }
}
