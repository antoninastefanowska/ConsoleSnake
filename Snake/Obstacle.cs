﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Obstacle : Entity
    {
        public Obstacle(List<Point> newPositions) : base(newPositions) { }
    }
}
