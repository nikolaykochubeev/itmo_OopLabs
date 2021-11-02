using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class MegaFacultyService
    {
        private readonly List<MegaFaculty> _megaFaculties;

        public MegaFacultyService(List<MegaFaculty> megaFaculty = null)
        {
            _megaFaculties = megaFaculty ?? new List<MegaFaculty>();
        }

        public IReadOnlyList<MegaFaculty> MegaFaculties => _megaFaculties;

        public MegaFaculty AddMegaFaculty(string name, string prefix)
        {
            if (_megaFaculties.FirstOrDefault(faculty => faculty.Prefix == prefix || faculty.Name == name) is not null)
                throw new IsuExtraException("MegaFaculty with this prefix or name already exists");
            _megaFaculties.Add(new MegaFaculty(prefix, name));
            return MegaFaculties.Last();
        }
    }
}