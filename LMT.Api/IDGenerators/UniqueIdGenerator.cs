using LMT.Api.Data;

namespace LMT.Api.IDGenerators
{
    public class UniqueIdGenerator
    {
        private static int _lastUniqueNumber;
        private static readonly object _lock = new object();
        private readonly EFDBContext _context;

        public UniqueIdGenerator(EFDBContext context)
        {
            _context = context;
            // Initialize from the database when the application starts
            _lastUniqueNumber = LoadLastUniqueNumberFromDb();
        }

        private  int LoadLastUniqueNumberFromDb()
        {
            // Retrieve the last unique code record, assuming it has a field called LastUniqueNumber
            var uniqueCodeRecord = _context.T_UniqueCodeRecords
                .FirstOrDefault();

            if (uniqueCodeRecord != null)
            {
                return uniqueCodeRecord.LastUniqueNumber; // Return the last unique number directly
            }

            return 0; // Default to 0 if no records found
        }

        public  string GenerateUniqueId()
        {
            var now = DateTime.Now;
            var day = now.ToString("dd");
            var month = now.ToString("MM");
            var year = now.ToString("yy");

            int uniqueNumber;
            lock (_lock) // Ensure thread safety for unique number increment
            {
                // Increment and wrap around after 99
                _lastUniqueNumber = (_lastUniqueNumber + 1) % 100;
                uniqueNumber = _lastUniqueNumber;

                // Save the new last unique number to the database
                SaveLastUniqueNumberToDb(uniqueNumber);
            }

            return $"LMS{year}{month}{day}{uniqueNumber:D2}";
        }

        private void SaveLastUniqueNumberToDb(int uniqueNumber)
        {
            // Here, we could either update an existing record or insert a new one.
            // This assumes you have a separate table or a way to track the last unique number.
            var uniqueCodeRecord = _context.T_UniqueCodeRecords.FirstOrDefault();
            if (uniqueCodeRecord == null)
            {
                uniqueCodeRecord = new Api.Entities.T_UniqueCodeRecords { LastUniqueNumber = uniqueNumber };
                _context.T_UniqueCodeRecords.Add(uniqueCodeRecord);
            }
            else
            {
                uniqueCodeRecord.LastUniqueNumber = uniqueNumber;
                _context.T_UniqueCodeRecords.Update(uniqueCodeRecord);
            }

            _context.SaveChanges(); // Persist the last unique number in the database
        }
    }
}
