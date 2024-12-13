using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Data;
using StudentPortal.web.Models;
using StudentPortal.web.Models.Entities;
using StudentPortal.web.Services;


namespace Enrollment_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IStudentService _studentService;

        public StudentController(ApplicationDbContext db, IStudentService studentService)
        {
            this._db = db;
            _studentService = studentService;
        }
		public IActionResult Menu()
		{
            ViewBag.IsUpdateMode = true; 
            return View();
        }

        public IActionResult SubjectEntryMenu()
        {
            ViewBag.IsUpdateMode = true; 
            return View();
        }

        public IActionResult SubjectScheduleEntryMenu()
        {
            ViewBag.IsUpdateMode = true; 
            return View();
        }

        public IActionResult Index(bool isUpdateMode = false, bool isDeleteMode = false)
        {
            List<Student> objStudentList = _db.Students.ToList();
            ViewBag.IsUpdateMode = isUpdateMode; 
            ViewBag.IsDeleteMode = isDeleteMode;
            return View(objStudentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel viewModel)
        {
            var student = new Student
            {
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                Middlename = viewModel.Middlename,
                Course = viewModel.Course,
                Year = viewModel.Year,
            };
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        

        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _db.Students
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    firstname = s.Firstname,
                    lastname = s.Lastname,
                    course = s.Course,
                    year = s.Year
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Json(student);
        }

        [HttpGet]
        public IActionResult EnrollmentEntry()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EnrollmentEntry(EnrollmentEntryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with the validation error messages.
                return View(viewModel);
            }

            var student = new EnrollmentEntry
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Course = viewModel.Course,
                Year = viewModel.Year,
                EDPCode = viewModel.EDPCode,
                SubjectCode = viewModel.SubjectCode,
                TimeStart = viewModel.TimeStart,
                TimeEnd = viewModel.TimeEnd,
                Days = viewModel.Days,
                Room = viewModel.Room,
                Units = viewModel.Units,
                Encoder = viewModel.Encoder,
                Date = viewModel.Date,
                TotalUnits = viewModel.TotalUnits,
            };

            await _db.EnrollmentEntry.AddAsync(student);
            await _db.SaveChangesAsync();

            return RedirectToAction("EnrollmentEntry");
        }

        [HttpGet]
        public async Task<IActionResult> GetScheduleEntries(string edpCode)
        {
            if (string.IsNullOrEmpty(edpCode))
            {
                return BadRequest("EDP Code is required.");
            }

            var schedules = await _db.Schedule
                .Where(s => s.SubjectEDPCode == edpCode)
                .Select(s => new
                {
                    s.SubjectEDPCode,
                    s.SubjectCode,
                    s.TimeStart,
                    s.TimeEnd,
                    s.Days,
                    s.Room,
                    s.Description // Assuming "Units" is stored in Description.
                })
                .ToListAsync();

            return Json(schedules);
        }




        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student viewmodel)
        {
            var student = await _db.Students.FindAsync(viewmodel.Id);
            if(student is not null)
            {
                student.Firstname = viewmodel.Firstname;
                student.Lastname = viewmodel.Lastname;
                student.Middlename = viewmodel.Middlename;
                student.Course = viewmodel.Course;
                student.Year = viewmodel.Year;

                await _db.SaveChangesAsync();
               
            }
            ViewBag.IsUpdateMode = false;

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete()
        {
            return RedirectToAction("Index", new { isDeleteMode = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                TempData["success"] = "Student deleted successfully.";
            }
            else
            {
                TempData["error"] = "Student not found.";
            }
            return RedirectToAction("Index");
        }

        public IActionResult SubjectEntryTable(bool isUpdateMode = false, bool isDeleteMode = false)
        {
            List<SubjectEntry> objSubjectList = _db.AddingSubjectEntries.ToList();
            ViewBag.IsUpdateMode = isUpdateMode; 
            ViewBag.IsDeleteMode = isDeleteMode;
            return View(objSubjectList);
        }


        [HttpGet]
        public IActionResult SubjectEntry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubjectEntry(SubjectEntryViewModel viewModel)
        {
            var subjectentry = new SubjectEntry
            {
                Id = viewModel.Id,
                Subject = viewModel.Subject,
                Description = viewModel.Description,
                Units = viewModel.Units,
                Offering = viewModel.Offering,
                Category = viewModel.Category,
                CourseCode = viewModel.CourseCode,
                CurriculumYear = viewModel.CurriculumYear,
                SubjectCode = viewModel.SubjectCode,
                PreCorReq = viewModel.PreCorReq,
            };
            await _db.AddingSubjectEntries.AddAsync(subjectentry);
            await _db.SaveChangesAsync();

            var subjectentries = await _db.AddingSubjectEntries.ToListAsync();
            return RedirectToAction("SubjectEntryTable", subjectentries);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubjectEntry(int id)
        {
            var subjectEntry = await _db.AddingSubjectEntries.FindAsync(id);
            if (subjectEntry == null)
            {
                return NotFound();
            }
            ViewBag.IsUpdateMode = true;
            return View(subjectEntry);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubjectEntry(SubjectEntry viewModel)
        {
            var subjectEntry = await _db.AddingSubjectEntries.FindAsync(viewModel.Id);
            if (subjectEntry != null)
            {
                subjectEntry.Id = viewModel.Id;
                subjectEntry.Subject = viewModel.Subject;
                subjectEntry.Description = viewModel.Description;
                subjectEntry.Units = viewModel.Units;
                subjectEntry.Offering = viewModel.Offering;
                subjectEntry.Category = viewModel.Category;
                subjectEntry.CourseCode = viewModel.CourseCode;
                subjectEntry.CurriculumYear = viewModel.CurriculumYear;
                subjectEntry.SubjectCode = viewModel.SubjectCode;
                subjectEntry.PreCorReq = viewModel.PreCorReq;



                await _db.SaveChangesAsync();
            }
            ViewBag.IsUpdateMode = true;
            return RedirectToAction("SubjectEntryTable");
        }



        [HttpGet]
        public IActionResult DeleteSubjectEntry(int id)
        {
            return RedirectToAction("SubjectEntryTable", new { isDeleteMode = true });
        }

        [HttpPost]
        public IActionResult DeleteSubjectEntry(int id, bool isDeleteMode)
        {
            var subjectEntry = _db.AddingSubjectEntries.FirstOrDefault(s => s.Id == id);

            if (subjectEntry != null)
            {
                _db.AddingSubjectEntries.Remove(subjectEntry);
                _db.SaveChanges();

                TempData["success"] = "Subject entry deleted successfully.";  
            }
            else
            {
                TempData["error"] = "Subject entry not found."; 
            }

            return RedirectToAction("SubjectEntryTable", new { isDeleteMode = isDeleteMode });
        }
        
        public IActionResult SubjectScheduleEntryTable(bool isUpdateMode = false, bool isDeleteMode = false)
        {
            List<Schedule> objSubjectScheduleList = _db.Schedule.ToList();
            ViewBag.IsUpdateMode = isUpdateMode; 
            ViewBag.IsDeleteMode = isDeleteMode;
            return View(objSubjectScheduleList);
        }

        [HttpGet]
        public IActionResult SubjectScheduleEntry()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> SubjectScheduleEntry(Schedule viewModel)
        {
            var subjectScheduleEntry = new Schedule
            {
                Id = viewModel.Id,
                SubjectEDPCode = viewModel.SubjectEDPCode,
                SubjectCode = viewModel.SubjectCode,
                Description = viewModel.Description,
                TimeStart = viewModel.TimeStart,
                TimeEnd = viewModel.TimeEnd,
                Days = viewModel.Days,
                Section = viewModel.Section,
                Room = viewModel.Room,
                Year = viewModel.Year,
                AmPm = viewModel.AmPm,
            };

            // Add the new entry to the database
            await _db.Schedule.AddAsync(subjectScheduleEntry);
            await _db.SaveChangesAsync();

            // Retrieve the updated list of subject schedule entries
            var subjectScheduleEntries = await _db.Schedule.ToListAsync();

            // Return the updated list to the view
            return View("SubjectScheduleEntryTable", subjectScheduleEntries);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubjectScheduleEntry(int id)
        {
            var subjectEntry = await _db.Schedule.FindAsync(id);
            if (subjectEntry == null)
            {
                return NotFound();
            }
            ViewBag.IsUpdateMode = true;
            return View(subjectEntry);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubjectScheduleEntry(Schedule viewModel)
        {
            var subjectScheduleEntry = await _db.Schedule.FindAsync(viewModel.Id);
            if (subjectScheduleEntry != null)
            {
                subjectScheduleEntry.Id = viewModel.Id;
                subjectScheduleEntry.SubjectEDPCode = viewModel.SubjectEDPCode;
                subjectScheduleEntry.SubjectCode = viewModel.SubjectCode;
                subjectScheduleEntry.Description = viewModel.Description;
                subjectScheduleEntry.TimeStart = viewModel.TimeStart;
                subjectScheduleEntry.TimeEnd = viewModel.TimeEnd;
                subjectScheduleEntry.Days = viewModel.Days;
                subjectScheduleEntry.Section = viewModel.Section;
                subjectScheduleEntry.Room = viewModel.Room;
                subjectScheduleEntry.Year = viewModel.Year;
                subjectScheduleEntry.AmPm = viewModel.AmPm;

                await _db.SaveChangesAsync();
            }
            ViewBag.IsUpdateMode = true;
            return RedirectToAction("SubjectScheduleEntryTable");
        }

        [HttpGet]
        public IActionResult DeleteSubjectScheduleEntry(int id)
        {
            return RedirectToAction("SubjectScheduleEntryTable", new { isDeleteMode = true });
        }

        [HttpPost]
        public IActionResult DeleteSubjectScheduleEntry(int id, bool isDeleteMode)
        {
            var subjectScheduleEntry = _db.Schedule.FirstOrDefault(s => s.Id == id);

            if (subjectScheduleEntry != null)
            {
                _db.Schedule.Remove(subjectScheduleEntry);
                _db.SaveChanges();

                TempData["success"] = "Subject Schedule entry deleted successfully.";  
            }
            else
            {
                TempData["error"] = "Subject Schedule entry not found.";  
            }

            return RedirectToAction("SubjectScheduleEntryTable", new { isDeleteMode = isDeleteMode });
        }

    }
}