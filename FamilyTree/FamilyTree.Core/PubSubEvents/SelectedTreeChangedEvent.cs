﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.PubSubEvents
{
    public class SelectedTreeChangedEvent : PubSubEvent<Business.FamilyTree>
    {
    }
}
