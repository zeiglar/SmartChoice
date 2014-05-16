using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Enums
{
    public enum ErrorCodes
    {
        //Source
        Default = 0x00,
        Error_WebPageDownloading = 0x01,

        //Web UI
        Error_IncorrectAppSetting = 0x21,
        Error_CannotFindFile = 0x22,
        Error_CannotCreateFile,
        Error_FileHasNoContent
    }
}
