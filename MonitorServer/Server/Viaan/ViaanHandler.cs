using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ViaanHandler : BaseHandler
    {
        public ViaanHandler() : base() { }
        
        public override void HandlePackage(byte[] bytes, IntPtr connId)
        {
            throw new NotImplementedException();
        }

        public override void SubPackage(ByteBuffer byteBuffer, IntPtr connId)
        {
            throw new NotImplementedException();
        }
    }
}
