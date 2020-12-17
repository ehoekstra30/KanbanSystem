using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanDal;


namespace KanbanAndon
{
    class WorkstationReader
    {
        // Query to find an available workstation
        private IQueryable<Workstation> queryWorkstations;
        private KanbanDbModel kdb;


        public WorkstationReader(KanbanDbModel kdb) {

            this.kdb = kdb;

            // Query to find an available workstation
            queryWorkstations =
                from station in this.kdb.Workstations
                where station.IsCurrentlyWorking == true
                select station;
        }

        public List<Workstation> GetWorkstations() {

            return queryWorkstations.ToList();
        }

    }
}
