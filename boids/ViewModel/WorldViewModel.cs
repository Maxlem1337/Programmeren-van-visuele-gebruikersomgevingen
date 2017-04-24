﻿using Bindings;
using Cells;
using Mathematics;
using Model;
using Model.Species;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WorldViewModel
    {
        public readonly Simulation Simulation;

        public WorldViewModel()
        {
            this.Simulation = new Simulation();
            this.Simulation.Species[0].CreateBoid(new Vector2D(50, 50));
            this.Simulation.Species[1].CreateBoid(new Vector2D(150, 150));
        }

        /*
        public ObservableCollection<BoidViewModel> Population
        {
            get
            {
                ObservableCollection<BoidViewModel> pop = new ObservableCollection<BoidViewModel>();

                foreach (Boid boid in Simulation.World.Population)
                {
                    pop.Add(new BoidViewModel(boid));
                }

                return pop;
            }
        }
        */
        /*
        public ObservableCollection<BoidViewModel> Population
        {
            get
            {
                return Simulation.World.Population;
            }
        }
        */

        public IEnumerable<BoidViewModel> Population
        {
            get
            {
                return Simulation.World.Population.Select(b => new BoidViewModel(b));
            }
        }

        public IEnumerable<SpeciesViewModel> Species
        {
            get
            {
                return Simulation.Species.Select(s => new SpeciesViewModel(s));
            }
            set
            {

            }
        }


        public double test
        {
            get
            {
                return Simulation.World.Population[0].Species.Bindings.Read(BoidSpecies.MaximumSpeed).Value;
            }
            set
            {
                Simulation.World.Population[0].Species.Bindings.Read(BoidSpecies.MaximumSpeed).Value = value;
            }

        }

    }
}
