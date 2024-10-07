using LMT.Api.Data;

namespace LMT.Api.IDGenerators
{
    public class UniqueIdGeneratorForWorkers
    {
        private static int _lastUniqueNumber;
        private static readonly object _lock = new object();
        private readonly EFDBContext _context;

        public UniqueIdGeneratorForWorkers(EFDBContext context)
        {
            _context = context;
            // Initialize from the database when the application starts
            _lastUniqueNumber = LoadLastUniqueNumberFromDb();
        }

        private int LoadLastUniqueNumberFromDb()
        {
            // Retrieve the last unique code record, assuming it has a field called LastUniqueNumber
            var uniqueCodeRecord = _context.T_UniqueCodeRecordsForWorkers
                .FirstOrDefault();

            if (uniqueCodeRecord != null)
            {
                return uniqueCodeRecord.LastUniqueNumber; // Return the last unique number directly
            }

            return 0; // Default to 0 if no records found
        }

        public string GenerateUniqueId()
        {
            int uniqueNumber;
            lock (_lock)
            {
                _lastUniqueNumber += 1;
                SaveLastUniqueNumberToDb(_lastUniqueNumber);
                uniqueNumber = _lastUniqueNumber;
            }

            return $"SLA{uniqueNumber:D6}";
        }

        private void SaveLastUniqueNumberToDb(int uniqueNumber)
        {
            // Here, we could either update an existing record or insert a new one.
            // This assumes you have a separate table or a way to track the last unique number.
            var uniqueCodeRecord = _context.T_UniqueCodeRecordsForWorkers.FirstOrDefault();
            if (uniqueCodeRecord == null)
            {
                uniqueCodeRecord = new Api.Entities.T_UniqueCodeRecordsForWorkers { LastUniqueNumber = uniqueNumber };
                _context.T_UniqueCodeRecordsForWorkers.Add(uniqueCodeRecord);
            }
            else
            {
                uniqueCodeRecord.LastUniqueNumber = uniqueNumber;
                _context.T_UniqueCodeRecordsForWorkers.Update(uniqueCodeRecord);
            }

            _context.SaveChanges(); // Persist the last unique number in the database
        }
    }
}
