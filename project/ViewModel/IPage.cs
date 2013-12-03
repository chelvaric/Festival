using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.ViewModel
{
    //elke viewmodel class moet deze interface implementeren 
    //zo kan ik later een lijst van objecten van classes gaan bijhouden 
    //waarvan deze class implemneteerd deze lijst zit in ApplicationVM
    interface IPage
    {
        //één property insteken elke klasse moet deze prop gaan uitwerken
        string Name { get; }
    }
}
