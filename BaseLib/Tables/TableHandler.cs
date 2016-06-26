using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BaseLib.Tables
{
    /** This class handles the brunt of loading the binary table files
     *  Every specific table will have their own custom loading method
     *  derriving from this base class.  
     **/
    class BaseTableHandler
    {
        int load(string rdf_file, uint record_size)
        {
            byte padding;
            char[] record;
            int ret;
            uint data_size;
            BinaryReader f = new BinaryReader(File.Open(rdf_file, FileMode.Open));

            if (f == null)
            {
                return -1;
            }


		/* Read file header information then ignore it*/
    		f.ReadByte();
            f.ReadInt32();
            f.ReadByte();

            record = new char[record_size - 1];
            /* Wrote custom memset method to handle 0ing out records */
            Helpers.Util.Memset(record, 0, record_size);

            /* This loops through the file reading a record into memory */
            while (f.BaseStream.Position != f.BaseStream.Length)
            {
                int n = (int)record_size;
                ret = f.Read(record, 0, n);

                if (ret == 1)
                {
                    // Havee to write the on_record function as an abstract in the base class.
                    //if (on_record(record, (uint)ret) != 0)
                    //{
                        return -1;
                    //}
                }
                else if (f.BaseStream.Position != f.BaseStream.Length)
                {
                    SysCons.LogError("%s: error loading [%s]: read %d vs %d\n", System.Reflection.MethodBase.GetCurrentMethod().Name, rdf_file, record_size, ret);
                    return -1;
                }
            }

            record = null;
            f = null;

            return 0;
        }




        /* return 0 means good, anything else means it should stop reading */
        //protected abstract int on_record(object record, uint record_size);

    }
}
