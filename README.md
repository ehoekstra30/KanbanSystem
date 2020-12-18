## Ethan & Mohamed's Kanban System

Usage information for our solution for Advanced SQL's final project.


### Preparation

Make sure you have SQL Server serving up an instance of the database. To build or
renew the database, simply run the SQL script contained in KanbanDal/sqlscripts on
your SQL Server instance. This script will create the kanbandb database required
for the rest of the system to communicate through.


### Program Structure


#### Workstation Simulator

The bread & butter program of the entire system. One instance of the workstation
simulator represents one workstation on the factory floor. Initialize at least one
instance of this program to populate the database with foglamps the workstation
is creating. Number of initializations of WorkstationSimulator is limited by a 
setting found in the ConfigTool.


#### Andon Display

Although the WorkstationSimulator supplies graphical representation of the bin counts
anyway, this program is also created to do the same. Initialization of this program is
optional, but can be used to monitor WorkstationSimulator progress.


#### Kanban Board

Once the workstation(s) have populated the database with foglamps, initialize an instance
of the Kanban Board program to randomly create and fulfill orders for foglamps. Only one
instance of the Kanban Board can be run at a time. The Kanban Board will automatically
populate its columns with foglamps in some step along the production line (untested, 
failed or passed testing).

