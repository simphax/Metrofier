using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Metrofier.Metadata
{
    [ServiceContract]
    public interface IMetrofierService
    {
        //Starts a new process for file, returns processId
        [OperationContract]
        uint Start(string file, string directory);

        [OperationContract]
        bool Show(uint processId);

        [OperationContract]
        bool Hide(uint processId);

        [OperationContract]
        bool Close(uint processId);

        [OperationContract]
        bool Resize(uint processId, int width, int height);
    }
}
