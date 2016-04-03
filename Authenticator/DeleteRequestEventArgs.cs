﻿using Authenticator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    public class DeleteRequestEventArgs : EventArgs
    {
        public Entry Entry { get; }

        public DeleteRequestEventArgs(Entry entry)
        {
            Entry = entry;
        }
    }
}
