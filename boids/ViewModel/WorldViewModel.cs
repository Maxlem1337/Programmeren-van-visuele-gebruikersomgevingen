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
        private readonly Simulation Simulation;
        public bool Pause { get; set; }

        public WorldViewModel()
        {
            this.Simulation = new Simulation();
            this.Simulation.Species[0].CreateBoid(new Vector2D(50, 50));
            this.Simulation.Species[1].CreateBoid(new Vector2D(150, 150));

            
            this.Population = Cell.Create<IEnumerable<BoidViewModel>>(null);
            RefreshPopulation();
            //this.Species = Cell.Create<IEnumerable<SpeciesViewModel>>(null);

            this.Simulation.World.Population.CollectionChanged += Population_CollectionChanged;
            
        }

        private void Population_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RefreshPopulation();
        }


        //public IEnumerable<BoidViewModel> Population
        //{
        //    get
        //    {
        //        return Simulation.World.Population.Select(b => new BoidViewModel(b));
        //    }
        //}

        public Cell<IEnumerable<BoidViewModel>> Population { get; }

        public void RefreshPopulation()
        {
            this.Population.Value = Simulation.World.Population.Select(b => new BoidViewModel(b));
        }



        public IEnumerable<SpeciesViewModel> Species
        {
            get
            {
                return Simulation.Species.Select(s => new SpeciesViewModel(s));
            }
        }

        /*
        public Cell<IEnumerable<SpeciesViewModel>> Species { get; }

        public void RefreshSpecies()
        {
            this.Species.Value = Simulation.Species.Select(s => new SpeciesViewModel(s));
        }
        */

        public void Update(double tijd)
        {
            if (!Pause)
            {
                Simulation.Update(tijd);
            }
        }

        public List<IParameterViewModel> Parameters
        {
            get
            {
                //return BoidSpecies.Bindings.Parameters;
                List<IParameterViewModel> paramlist = new List<IParameterViewModel>();
                foreach (var param in Simulation.World.Bindings.Parameters)
                {
                    dynamic dynamicparam = param;
                    paramlist.Add(CreateParameterViewModel(dynamicparam));
                }
                return paramlist;
            }
        }

        public IParameterViewModel CreateParameterViewModel(IParameter p)
        {
            dynamic q = p;
            return CreateParameterViewModel(q);
        }

        private IParameterViewModel CreateParameterViewModel(RangedDoubleParameter p)
        {
            Cell<double> test = Simulation.World.Bindings.Read(p);
            return new RangedDoubleParameterViewModel(p, test);
        }

        public Cell<double> Width
        {
            get
            {
                return Simulation.World.Bindings.Read(World.Width);
            }
        }

        public Cell<double> Height
        {
            get
            {
                return Simulation.World.Bindings.Read(World.Height);
            }
        }
    }
}
