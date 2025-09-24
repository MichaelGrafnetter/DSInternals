namespace DSInternals.Common.Interop
{
    /// <summary>
    /// Win32 Error Code
    /// </summary>
    /// <see>http://msdn.microsoft.com/en-us/library/cc231199.aspx</see>
    public enum Win32ErrorCode : int
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Incorrect function.
        /// </summary>
        INVALID_FUNCTION = 1,

        /// <summary>
        /// The system cannot find the file specified.
        /// </summary>
        FILE_NOT_FOUND = 2,

        /// <summary>
        /// The system cannot find the path specified.
        /// </summary>
        PATH_NOT_FOUND = 3,

        /// <summary>
        /// The system cannot open the file.
        /// </summary>
        TOO_MANY_OPEN_FILES = 4,

        /// <summary>
        /// Access is denied.
        /// </summary>
        ACCESS_DENIED = 5,

        /// <summary>
        /// The handle is invalid.
        /// </summary>
        INVALID_HANDLE = 6,

        /// <summary>
        /// The storage control blocks were destroyed.
        /// </summary>
        ARENA_TRASHED = 7,

        /// <summary>
        /// Not enough storage is available to process this command.
        /// </summary>
        NOT_ENOUGH_MEMORY = 8,

        /// <summary>
        /// The storage control block address is invalid.
        /// </summary>
        INVALID_BLOCK = 9,

        /// <summary>
        /// The environment is incorrect.
        /// </summary>
        BAD_ENVIRONMENT = 10,

        /// <summary>
        /// An attempt was made to load a program with an incorrect format.
        /// </summary>
        BAD_FORMAT = 11,

        /// <summary>
        /// The access code is invalid.
        /// </summary>
        INVALID_ACCESS = 12,

        /// <summary>
        /// The data is invalid.
        /// </summary>
        INVALID_DATA = 13,

        /// <summary>
        /// Not enough storage is available to complete this operation.
        /// </summary>
        OUTOFMEMORY = 14,

        /// <summary>
        /// The system cannot find the drive specified.
        /// </summary>
        INVALID_DRIVE = 15,

        /// <summary>
        /// The directory cannot be removed.
        /// </summary>
        CURRENT_DIRECTORY = 16,

        /// <summary>
        /// The system cannot move the file to a different disk drive.
        /// </summary>
        NOT_SAME_DEVICE = 17,

        /// <summary>
        /// There are no more files.
        /// </summary>
        NO_MORE_FILES = 18,

        /// <summary>
        /// The media is write protected.
        /// </summary>
        WRITE_PROTECT = 19,

        /// <summary>
        /// The system cannot find the device specified.
        /// </summary>
        BAD_UNIT = 20,

        /// <summary>
        /// The device is not ready.
        /// </summary>
        NOT_READY = 21,

        /// <summary>
        /// The device does not recognize the command.
        /// </summary>
        BAD_COMMAND = 22,

        /// <summary>
        /// Data error (cyclic redundancy check).
        /// </summary>
        CRC = 23,

        /// <summary>
        /// The program issued a command but the command length is incorrect.
        /// </summary>
        BAD_LENGTH = 24,

        /// <summary>
        /// The drive cannot locate a specific area or track on the disk.
        /// </summary>
        SEEK = 25,

        /// <summary>
        /// The specified disk or diskette cannot be accessed.
        /// </summary>
        NOT_DOS_DISK = 26,

        /// <summary>
        /// The drive cannot find the sector requested.
        /// </summary>
        SECTOR_NOT_FOUND = 27,

        /// <summary>
        /// The printer is out of paper.
        /// </summary>
        OUT_OF_PAPER = 28,

        /// <summary>
        /// The system cannot write to the specified device.
        /// </summary>
        WRITE_FAULT = 29,

        /// <summary>
        /// The system cannot read from the specified device.
        /// </summary>
        READ_FAULT = 30,

        /// <summary>
        /// A device attached to the system is not functioning.
        /// </summary>
        GEN_FAILURE = 31,

        /// <summary>
        /// The process cannot access the file because it is being used by another process.
        /// </summary>
        SHARING_VIOLATION = 32,

        /// <summary>
        /// The process cannot access the file because another process has locked a portion of the file.
        /// </summary>
        LOCK_VIOLATION = 33,

        /// <summary>
        /// The wrong diskette is in the drive.
        /// Insert %2 (Volume Serial Number: %3) into drive %1.
        /// </summary>
        WRONG_DISK = 34,

        /// <summary>
        /// Too many files opened for sharing.
        /// </summary>
        SHARING_BUFFER_EXCEEDED = 36,

        /// <summary>
        /// Reached the end of the file.
        /// </summary>
        HANDLE_EOF = 38,

        /// <summary>
        /// The disk is full.
        /// </summary>
        HANDLE_DISK_FULL = 39,

        /// <summary>
        /// The request is not supported.
        /// </summary>
        NOT_SUPPORTED = 50,

        /// <summary>
        /// Windows cannot find the network path. Verify that the network path is correct and the destination computer is not busy or turned off. If Windows still cannot find the network path, contact your network administrator.
        /// </summary>
        REM_NOT_LIST = 51,

        /// <summary>
        /// You were not connected because a duplicate name exists on the network. Go to System in Control Panel to change the computer name and try again.
        /// </summary>
        DUP_NAME = 52,

        /// <summary>
        /// The network path was not found.
        /// </summary>
        BAD_NETPATH = 53,

        /// <summary>
        /// The network is busy.
        /// </summary>
        NETWORK_BUSY = 54,

        /// <summary>
        /// The specified network resource or device is no longer available.
        /// </summary>
        DEV_NOT_EXIST = 55,

        /// <summary>
        /// The network BIOS command limit has been reached.
        /// </summary>
        TOO_MANY_CMDS = 56,

        /// <summary>
        /// A network adapter hardware error occurred.
        /// </summary>
        ADAP_HDW_ERR = 57,

        /// <summary>
        /// The specified server cannot perform the requested operation.
        /// </summary>
        BAD_NET_RESP = 58,

        /// <summary>
        /// An unexpected network error occurred.
        /// </summary>
        UNEXP_NET_ERR = 59,

        /// <summary>
        /// The remote adapter is not compatible.
        /// </summary>
        BAD_REM_ADAP = 60,

        /// <summary>
        /// The printer queue is full.
        /// </summary>
        PRINTQ_FULL = 61,

        /// <summary>
        /// Space to store the file waiting to be printed is not available on the server.
        /// </summary>
        NO_SPOOL_SPACE = 62,

        /// <summary>
        /// Your file waiting to be printed was deleted.
        /// </summary>
        PRINT_CANCELLED = 63,

        /// <summary>
        /// The specified network name is no longer available.
        /// </summary>
        NETNAME_DELETED = 64,

        /// <summary>
        /// Network access is denied.
        /// </summary>
        NETWORK_ACCESS_DENIED = 65,

        /// <summary>
        /// The network resource type is not correct.
        /// </summary>
        BAD_DEV_TYPE = 66,

        /// <summary>
        /// The network name cannot be found.
        /// </summary>
        BAD_NET_NAME = 67,

        /// <summary>
        /// The name limit for the local computer network adapter card was exceeded.
        /// </summary>
        TOO_MANY_NAMES = 68,

        /// <summary>
        /// The network BIOS session limit was exceeded.
        /// </summary>
        TOO_MANY_SESS = 69,

        /// <summary>
        /// The remote server has been paused or is in the process of being started.
        /// </summary>
        SHARING_PAUSED = 70,

        /// <summary>
        /// No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept.
        /// </summary>
        REQ_NOT_ACCEP = 71,

        /// <summary>
        /// The specified printer or disk device has been paused.
        /// </summary>
        REDIR_PAUSED = 72,

        /// <summary>
        /// The file exists.
        /// </summary>
        FILE_EXISTS = 80,

        /// <summary>
        /// The directory or file cannot be created.
        /// </summary>
        CANNOT_MAKE = 82,

        /// <summary>
        /// Fail on INT 24.
        /// </summary>
        FAIL_I24 = 83,

        /// <summary>
        /// Storage to process this request is not available.
        /// </summary>
        OUT_OF_STRUCTURES = 84,

        /// <summary>
        /// The local device name is already in use.
        /// </summary>
        ALREADY_ASSIGNED = 85,

        /// <summary>
        /// The specified network password is not correct.
        /// </summary>
        INVALID_PASSWORD = 86,

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        INVALID_PARAMETER = 87,

        /// <summary>
        /// A write fault occurred on the network.
        /// </summary>
        NET_WRITE_FAULT = 88,

        /// <summary>
        /// The system cannot start another process at this time.
        /// </summary>
        NO_PROC_SLOTS = 89,

        /// <summary>
        /// Cannot create another system semaphore.
        /// </summary>
        TOO_MANY_SEMAPHORES = 100,

        /// <summary>
        /// The exclusive semaphore is owned by another process.
        /// </summary>
        EXCL_SEM_ALREADY_OWNED = 101,

        /// <summary>
        /// The semaphore is set and cannot be closed.
        /// </summary>
        SEM_IS_SET = 102,

        /// <summary>
        /// The semaphore cannot be set again.
        /// </summary>
        TOO_MANY_SEM_REQUESTS = 103,

        /// <summary>
        /// Cannot request exclusive semaphores at interrupt time.
        /// </summary>
        INVALID_AT_INTERRUPT_TIME = 104,

        /// <summary>
        /// The previous ownership of this semaphore has ended.
        /// </summary>
        SEM_OWNER_DIED = 105,

        /// <summary>
        /// Insert the diskette for drive %1.
        /// </summary>
        SEM_USER_LIMIT = 106,

        /// <summary>
        /// The program stopped because an alternate diskette was not inserted.
        /// </summary>
        DISK_CHANGE = 107,

        /// <summary>
        /// The disk is in use or locked by another process.
        /// </summary>
        DRIVE_LOCKED = 108,

        /// <summary>
        /// The pipe has been ended.
        /// </summary>
        BROKEN_PIPE = 109,

        /// <summary>
        /// The system cannot open the device or file specified.
        /// </summary>
        OPEN_FAILED = 110,

        /// <summary>
        /// The file name is too long.
        /// </summary>
        BUFFER_OVERFLOW = 111,

        /// <summary>
        /// There is not enough space on the disk.
        /// </summary>
        DISK_FULL = 112,

        /// <summary>
        /// No more internal file identifiers available.
        /// </summary>
        NO_MORE_SEARCH_HANDLES = 113,

        /// <summary>
        /// The target internal file identifier is incorrect.
        /// </summary>
        INVALID_TARGET_HANDLE = 114,

        /// <summary>
        /// The IOCTL call made by the application program is not correct.
        /// </summary>
        INVALID_CATEGORY = 117,

        /// <summary>
        /// The verify-on-write switch parameter value is not correct.
        /// </summary>
        INVALID_VERIFY_SWITCH = 118,

        /// <summary>
        /// The system does not support the command requested.
        /// </summary>
        BAD_DRIVER_LEVEL = 119,

        /// <summary>
        /// This function is not supported on this system.
        /// </summary>
        CALL_NOT_IMPLEMENTED = 120,

        /// <summary>
        /// The semaphore timeout period has expired.
        /// </summary>
        SEM_TIMEOUT = 121,

        /// <summary>
        /// The data area passed to a system call is too small.
        /// </summary>
        INSUFFICIENT_BUFFER = 122,

        /// <summary>
        /// The filename, directory name, or volume label syntax is incorrect.
        /// </summary>
        INVALID_NAME = 123,

        /// <summary>
        /// The system call level is not correct.
        /// </summary>
        INVALID_LEVEL = 124,

        /// <summary>
        /// The disk has no volume label.
        /// </summary>
        NO_VOLUME_LABEL = 125,

        /// <summary>
        /// The specified module could not be found.
        /// </summary>
        MOD_NOT_FOUND = 126,

        /// <summary>
        /// The specified procedure could not be found.
        /// </summary>
        PROC_NOT_FOUND = 127,

        /// <summary>
        /// There are no child processes to wait for.
        /// </summary>
        WAIT_NO_CHILDREN = 128,

        /// <summary>
        /// The %1 application cannot be run in Win32 mode.
        /// </summary>
        CHILD_NOT_COMPLETE = 129,

        /// <summary>
        /// Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O.
        /// </summary>
        DIRECT_ACCESS_HANDLE = 130,

        /// <summary>
        /// An attempt was made to move the file pointer before the beginning of the file.
        /// </summary>
        NEGATIVE_SEEK = 131,

        /// <summary>
        /// The file pointer cannot be set on the specified device or file.
        /// </summary>
        SEEK_ON_DEVICE = 132,

        /// <summary>
        /// A JOIN or SUBST command cannot be used for a drive that contains previously joined drives.
        /// </summary>
        IS_JOIN_TARGET = 133,

        /// <summary>
        /// An attempt was made to use a JOIN or SUBST command on a drive that has already been joined.
        /// </summary>
        IS_JOINED = 134,

        /// <summary>
        /// An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted.
        /// </summary>
        IS_SUBSTED = 135,

        /// <summary>
        /// The system tried to delete the JOIN of a drive that is not joined.
        /// </summary>
        NOT_JOINED = 136,

        /// <summary>
        /// The system tried to delete the substitution of a drive that is not substituted.
        /// </summary>
        NOT_SUBSTED = 137,

        /// <summary>
        /// The system tried to join a drive to a directory on a joined drive.
        /// </summary>
        JOIN_TO_JOIN = 138,

        /// <summary>
        /// The system tried to substitute a drive to a directory on a substituted drive.
        /// </summary>
        SUBST_TO_SUBST = 139,

        /// <summary>
        /// The system tried to join a drive to a directory on a substituted drive.
        /// </summary>
        JOIN_TO_SUBST = 140,

        /// <summary>
        /// The system tried to SUBST a drive to a directory on a joined drive.
        /// </summary>
        SUBST_TO_JOIN = 141,

        /// <summary>
        /// The system cannot perform a JOIN or SUBST at this time.
        /// </summary>
        BUSY_DRIVE = 142,

        /// <summary>
        /// The system cannot join or substitute a drive to or for a directory on the same drive.
        /// </summary>
        SAME_DRIVE = 143,

        /// <summary>
        /// The directory is not a subdirectory of the root directory.
        /// </summary>
        DIR_NOT_ROOT = 144,

        /// <summary>
        /// The directory is not empty.
        /// </summary>
        DIR_NOT_EMPTY = 145,

        /// <summary>
        /// The path specified is being used in a substitute.
        /// </summary>
        IS_SUBST_PATH = 146,

        /// <summary>
        /// Not enough resources are available to process this command.
        /// </summary>
        IS_JOIN_PATH = 147,

        /// <summary>
        /// The path specified cannot be used at this time.
        /// </summary>
        PATH_BUSY = 148,

        /// <summary>
        /// An attempt was made to join or substitute a drive for which a directory on the drive is the target of a previous substitute.
        /// </summary>
        IS_SUBST_TARGET = 149,

        /// <summary>
        /// System trace information was not specified in your CONFIG.SYS file, or tracing is disallowed.
        /// </summary>
        SYSTEM_TRACE = 150,

        /// <summary>
        /// The number of specified semaphore events for DosMuxSemWait is not correct.
        /// </summary>
        INVALID_EVENT_COUNT = 151,

        /// <summary>
        /// DosMuxSemWait did not execute, too many semaphores are already set.
        /// </summary>
        TOO_MANY_MUXWAITERS = 152,

        /// <summary>
        /// The DosMuxSemWait list is not correct.
        /// </summary>
        INVALID_LIST_FORMAT = 153,

        /// <summary>
        /// The volume label you entered exceeds the label character limit of the target file system.
        /// </summary>
        LABEL_TOO_LONG = 154,

        /// <summary>
        /// Cannot create another thread.
        /// </summary>
        TOO_MANY_TCBS = 155,

        /// <summary>
        /// The recipient process has refused the signal.
        /// </summary>
        SIGNAL_REFUSED = 156,

        /// <summary>
        /// The segment is already discarded and cannot be locked.
        /// </summary>
        DISCARDED = 157,

        /// <summary>
        /// The segment is already unlocked.
        /// </summary>
        NOT_LOCKED = 158,

        /// <summary>
        /// The address for the thread ID is not correct.
        /// </summary>
        BAD_THREADID_ADDR = 159,

        /// <summary>
        /// One or more arguments are not correct.
        /// </summary>
        BAD_ARGUMENTS = 160,

        /// <summary>
        /// The specified path is invalid.
        /// </summary>
        BAD_PATHNAME = 161,

        /// <summary>
        /// A signal is already pending.
        /// </summary>
        SIGNAL_PENDING = 162,

        /// <summary>
        /// No more threads can be created in the system.
        /// </summary>
        MAX_THRDS_REACHED = 164,

        /// <summary>
        /// Unable to lock a region of a file.
        /// </summary>
        LOCK_FAILED = 167,

        /// <summary>
        /// The requested resource is in use.
        /// </summary>
        BUSY = 170,

        /// <summary>
        /// A lock request was not outstanding for the supplied cancel region.
        /// </summary>
        CANCEL_VIOLATION = 173,

        /// <summary>
        /// The file system does not support atomic changes to the lock type.
        /// </summary>
        ATOMIC_LOCKS_NOT_SUPPORTED = 174,

        /// <summary>
        /// The system detected a segment number that was not correct.
        /// </summary>
        INVALID_SEGMENT_NUMBER = 180,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_ORDINAL = 182,

        /// <summary>
        /// Cannot create a file when that file already exists.
        /// </summary>
        ALREADY_EXISTS = 183,

        /// <summary>
        /// The flag passed is not correct.
        /// </summary>
        INVALID_FLAG_NUMBER = 186,

        /// <summary>
        /// The specified system semaphore name was not found.
        /// </summary>
        SEM_NOT_FOUND = 187,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_STARTING_CODESEG = 188,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_STACKSEG = 189,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_MODULETYPE = 190,

        /// <summary>
        /// Cannot run %1 in Win32 mode.
        /// </summary>
        INVALID_EXE_SIGNATURE = 191,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        EXE_MARKED_INVALID = 192,

        /// <summary>
        /// %1 is not a valid Win32 application.

        /// </summary>
        BAD_EXE_FORMAT = 193,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        ITERATED_DATA_EXCEEDS_64k = 194,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_MINALLOCSIZE = 195,

        /// <summary>
        /// The operating system cannot run this application program.
        /// </summary>
        DYNLINK_FROM_INVALID_RING = 196,

        /// <summary>
        /// The operating system is not presently configured to run this application.
        /// </summary>
        IOPL_NOT_ENABLED = 197,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INVALID_SEGDPL = 198,

        /// <summary>
        /// The operating system cannot run this application program.
        /// </summary>
        AUTODATASEG_EXCEEDS_64k = 199,

        /// <summary>
        /// The code segment cannot be greater than or equal to 64K.
        /// </summary>
        RING2SEG_MUST_BE_MOVABLE = 200,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        RELOC_CHAIN_XEEDS_SEGLIM = 201,

        /// <summary>
        /// The operating system cannot run %1.
        /// </summary>
        INFLOOP_IN_RELOC_CHAIN = 202,

        /// <summary>
        /// The system could not find the environment option that was entered.
        /// </summary>
        ENVVAR_NOT_FOUND = 203,

        /// <summary>
        /// No process in the command subtree has a signal handler.
        /// </summary>
        NO_SIGNAL_SENT = 205,

        /// <summary>
        /// The filename or extension is too long.
        /// </summary>
        FILENAME_EXCED_RANGE = 206,

        /// <summary>
        /// The ring 2 stack is in use.
        /// </summary>
        RING2_STACK_IN_USE = 207,

        /// <summary>
        /// The global filename characters, * or ?, are entered incorrectly or too many global filename characters are specified.
        /// </summary>
        META_EXPANSION_TOO_LONG = 208,

        /// <summary>
        /// The signal being posted is not correct.
        /// </summary>
        INVALID_SIGNAL_NUMBER = 209,

        /// <summary>
        /// The signal handler cannot be set.
        /// </summary>
        THREAD_1_INACTIVE = 210,

        /// <summary>
        /// The segment is locked and cannot be reallocated.
        /// </summary>
        LOCKED = 212,

        /// <summary>
        /// Too many dynamic-link modules are attached to this program or dynamic-link module.
        /// </summary>
        TOO_MANY_MODULES = 214,

        /// <summary>
        /// Cannot nest calls to LoadModule.
        /// </summary>
        NESTING_NOT_ALLOWED = 215,

        /// <summary>
        /// The image file %1 is valid, but is for a machine type other than the current machine.
        /// </summary>
        EXE_MACHINE_TYPE_MISMATCH = 216,

        /// <summary>
        /// No information avialable.
        /// </summary>
        EXE_CANNOT_MODIFY_SIGNED_BINARY = 217,

        /// <summary>
        /// No information avialable.
        /// </summary>
        EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 218,

        /// <summary>
        /// The pipe state is invalid.
        /// </summary>
        BAD_PIPE = 230,

        /// <summary>
        /// All pipe instances are busy.
        /// </summary>
        PIPE_BUSY = 231,

        /// <summary>
        /// The pipe is being closed.
        /// </summary>
        NO_DATA = 232,

        /// <summary>
        /// No process is on the other end of the pipe.
        /// </summary>
        PIPE_NOT_CONNECTED = 233,

        /// <summary>
        /// More data is available.
        /// </summary>
        MORE_DATA = 234,

        /// <summary>
        /// The session was canceled.
        /// </summary>
        VC_DISCONNECTED = 240,

        /// <summary>
        /// The specified extended attribute name was invalid.
        /// </summary>
        INVALID_EA_NAME = 254,

        /// <summary>
        /// The extended attributes are inconsistent.
        /// </summary>
        EA_LIST_INCONSISTENT = 255,

        /// <summary>
        /// The wait operation timed out.
        /// </summary>
        WAIT_TIMEOUT = 258,

        /// <summary>
        /// No more data is available.
        /// </summary>
        NO_MORE_ITEMS = 259,

        /// <summary>
        /// The copy functions cannot be used.
        /// </summary>
        CANNOT_COPY = 266,

        /// <summary>
        /// The directory name is invalid.
        /// </summary>
        DIRECTORY = 267,

        /// <summary>
        /// The extended attributes did not fit in the buffer.
        /// </summary>
        EAS_DIDNT_FIT = 275,

        /// <summary>
        /// The extended attribute file on the mounted file system is corrupt.
        /// </summary>
        EA_FILE_CORRUPT = 276,

        /// <summary>
        /// The extended attribute table file is full.
        /// </summary>
        EA_TABLE_FULL = 277,

        /// <summary>
        /// The specified extended attribute handle is invalid.
        /// </summary>
        INVALID_EA_HANDLE = 278,

        /// <summary>
        /// The mounted file system does not support extended attributes.
        /// </summary>
        EAS_NOT_SUPPORTED = 282,

        /// <summary>
        /// Attempt to release mutex not owned by caller.
        /// </summary>
        NOT_OWNER = 288,

        /// <summary>
        /// Too many posts were made to a semaphore.
        /// </summary>
        TOO_MANY_POSTS = 298,

        /// <summary>
        /// Only part of a ReadProcessMemory or WriteProcessMemory request was completed.
        /// </summary>
        PARTIAL_COPY = 299,

        /// <summary>
        /// The oplock request is denied.
        /// </summary>
        OPLOCK_NOT_GRANTED = 300,

        /// <summary>
        /// An invalid oplock acknowledgment was received by the system.
        /// </summary>
        INVALID_OPLOCK_PROTOCOL = 301,

        /// <summary>
        /// The volume is too fragmented to complete this operation.
        /// </summary>
        DISK_TOO_FRAGMENTED = 302,

        /// <summary>
        /// The file cannot be opened because it is in the process of being deleted.
        /// </summary>
        DELETE_PENDING = 303,

        /// <summary>
        /// The system cannot find message text for message number 0x%1 in the message file for %2.
        /// </summary>
        MR_MID_NOT_FOUND = 317,

        /// <summary>
        /// No information avialable.
        /// </summary>
        SCOPE_NOT_FOUND = 318,

        /// <summary>
        /// Attempt to access invalid address.
        /// </summary>
        INVALID_ADDRESS = 487,

        /// <summary>
        /// Arithmetic result exceeded 32 bits.
        /// </summary>
        ARITHMETIC_OVERFLOW = 534,

        /// <summary>
        /// There is a process on other end of the pipe.
        /// </summary>
        PIPE_CONNECTED = 535,

        /// <summary>
        /// Waiting for a process to open the other end of the pipe.
        /// </summary>
        PIPE_LISTENING = 536,

        /// <summary>
        /// Access to the extended attribute was denied.
        /// </summary>
        EA_ACCESS_DENIED = 994,

        /// <summary>
        /// The I/O operation has been aborted because of either a thread exit or an application request.
        /// </summary>
        OPERATION_ABORTED = 995,

        /// <summary>
        /// Overlapped I/O event is not in a signaled state.
        /// </summary>
        IO_INCOMPLETE = 996,

        /// <summary>
        /// Overlapped I/O operation is in progress.
        /// </summary>
        IO_PENDING = 997,

        /// <summary>
        /// Invalid access to memory location.
        /// </summary>
        NOACCESS = 998,

        /// <summary>
        /// Error performing inpage operation.
        /// </summary>
        SWAPERROR = 999,

        /// <summary>
        /// Recursion too deep, the stack overflowed.
        /// </summary>
        STACK_OVERFLOW = 1001,

        /// <summary>
        /// The window cannot act on the sent message.
        /// </summary>
        INVALID_MESSAGE = 1002,

        /// <summary>
        /// Cannot complete this function.
        /// </summary>
        CAN_NOT_COMPLETE = 1003,

        /// <summary>
        /// Invalid flags.
        /// </summary>
        INVALID_FLAGS = 1004,

        /// <summary>
        /// The volume does not contain a recognized file system.
        /// Please make sure that all required file system drivers are loaded and that the volume is not corrupted.
        /// </summary>
        UNRECOGNIZED_VOLUME = 1005,

        /// <summary>
        /// The volume for a file has been externally altered so that the opened file is no longer valid.
        /// </summary>
        FILE_INVALID = 1006,

        /// <summary>
        /// The requested operation cannot be performed in full-screen mode.
        /// </summary>
        FULLSCREEN_MODE = 1007,

        /// <summary>
        /// An attempt was made to reference a token that does not exist.
        /// </summary>
        NO_TOKEN = 1008,

        /// <summary>
        /// The configuration registry database is corrupt.
        /// </summary>
        BADDB = 1009,

        /// <summary>
        /// The configuration registry key is invalid.
        /// </summary>
        BADKEY = 1010,

        /// <summary>
        /// The configuration registry key could not be opened.
        /// </summary>
        CANTOPEN = 1011,

        /// <summary>
        /// The configuration registry key could not be read.
        /// </summary>
        CANTREAD = 1012,

        /// <summary>
        /// The configuration registry key could not be written.
        /// </summary>
        CANTWRITE = 1013,

        /// <summary>
        /// One of the files in the registry database had to be recovered by use of a log or alternate copy. The recovery was successful.
        /// </summary>
        REGISTRY_RECOVERED = 1014,

        /// <summary>
        /// The registry is corrupted. The structure of one of the files containing registry data is corrupted, or the system's memory image of the file is corrupted, or the file could not be recovered because the alternate copy or log was absent or corrupted.
        /// </summary>
        REGISTRY_CORRUPT = 1015,

        /// <summary>
        /// An I/O operation initiated by the registry failed unrecoverably. The registry could not read in, or write out, or flush, one of the files that contain the system's image of the registry.
        /// </summary>
        REGISTRY_IO_FAILED = 1016,

        /// <summary>
        /// The system has attempted to load or restore a file into the registry, but the specified file is not in a registry file format.
        /// </summary>
        NOT_REGISTRY_FILE = 1017,

        /// <summary>
        /// Illegal operation attempted on a registry key that has been marked for deletion.
        /// </summary>
        KEY_DELETED = 1018,

        /// <summary>
        /// System could not allocate the required space in a registry log.
        /// </summary>
        NO_LOG_SPACE = 1019,

        /// <summary>
        /// Cannot create a symbolic link in a registry key that already has subkeys or values.
        /// </summary>
        KEY_HAS_CHILDREN = 1020,

        /// <summary>
        /// Cannot create a stable subkey under a volatile parent key.
        /// </summary>
        CHILD_MUST_BE_VOLATILE = 1021,

        /// <summary>
        /// A notify change request is being completed and the information is not being returned in the caller's buffer. The caller now needs to enumerate the files to find the changes.
        /// </summary>
        NOTIFY_ENUM_DIR = 1022,

        /// <summary>
        /// A stop control has been sent to a service that other running services are dependent on.
        /// </summary>
        DEPENDENT_SERVICES_RUNNING = 1051,

        /// <summary>
        /// The requested control is not valid for this service.
        /// </summary>
        INVALID_SERVICE_CONTROL = 1052,

        /// <summary>
        /// The service did not respond to the start or control request in a timely fashion.
        /// </summary>
        SERVICE_REQUEST_TIMEOUT = 1053,

        /// <summary>
        /// A thread could not be created for the service.
        /// </summary>
        SERVICE_NO_THREAD = 1054,

        /// <summary>
        /// The service database is locked.
        /// </summary>
        SERVICE_DATABASE_LOCKED = 1055,

        /// <summary>
        /// An instance of the service is already running.
        /// </summary>
        SERVICE_ALREADY_RUNNING = 1056,

        /// <summary>
        /// The account name is invalid or does not exist, or the password is invalid for the account name specified.
        /// </summary>
        INVALID_SERVICE_ACCOUNT = 1057,

        /// <summary>
        /// The service cannot be started, either because it is disabled or because it has no enabled devices associated with it.
        /// </summary>
        SERVICE_DISABLED = 1058,

        /// <summary>
        /// Circular service dependency was specified.
        /// </summary>
        CIRCULAR_DEPENDENCY = 1059,

        /// <summary>
        /// The specified service does not exist as an installed service.
        /// </summary>
        SERVICE_DOES_NOT_EXIST = 1060,

        /// <summary>
        /// The service cannot accept control messages at this time.
        /// </summary>
        SERVICE_CANNOT_ACCEPT_CTRL = 1061,

        /// <summary>
        /// The service has not been started.
        /// </summary>
        SERVICE_NOT_ACTIVE = 1062,

        /// <summary>
        /// The service process could not connect to the service controller.
        /// </summary>
        FAILED_SERVICE_CONTROLLER_CONNECT = 1063,

        /// <summary>
        /// An exception occurred in the service when handling the control request.
        /// </summary>
        EXCEPTION_IN_SERVICE = 1064,

        /// <summary>
        /// The database specified does not exist.
        /// </summary>
        DATABASE_DOES_NOT_EXIST = 1065,

        /// <summary>
        /// The service has returned a service-specific error code.
        /// </summary>
        SERVICE_SPECIFIC_ERROR = 1066,

        /// <summary>
        /// The process terminated unexpectedly.
        /// </summary>
        PROCESS_ABORTED = 1067,

        /// <summary>
        /// The dependency service or group failed to start.
        /// </summary>
        SERVICE_DEPENDENCY_FAIL = 1068,

        /// <summary>
        /// The service did not start due to a logon failure.
        /// </summary>
        SERVICE_LOGON_FAILED = 1069,

        /// <summary>
        /// After starting, the service hung in a start-pending state.
        /// </summary>
        SERVICE_START_HANG = 1070,

        /// <summary>
        /// The specified service database lock is invalid.
        /// </summary>
        INVALID_SERVICE_LOCK = 1071,

        /// <summary>
        /// The specified service has been marked for deletion.
        /// </summary>
        SERVICE_MARKED_FOR_DELETE = 1072,

        /// <summary>
        /// The specified service already exists.
        /// </summary>
        SERVICE_EXISTS = 1073,

        /// <summary>
        /// The system is currently running with the last-known-good configuration.
        /// </summary>
        ALREADY_RUNNING_LKG = 1074,

        /// <summary>
        /// The dependency service does not exist or has been marked for deletion.
        /// </summary>
        SERVICE_DEPENDENCY_DELETED = 1075,

        /// <summary>
        /// The current boot has already been accepted for use as the last-known-good control set.
        /// </summary>
        BOOT_ALREADY_ACCEPTED = 1076,

        /// <summary>
        /// No attempts to start the service have been made since the last boot.
        /// </summary>
        SERVICE_NEVER_STARTED = 1077,

        /// <summary>
        /// The name is already in use as either a service name or a service display name.
        /// </summary>
        DUPLICATE_SERVICE_NAME = 1078,

        /// <summary>
        /// The account specified for this service is different from the account specified for other services running in the same process.
        /// </summary>
        DIFFERENT_SERVICE_ACCOUNT = 1079,

        /// <summary>
        /// Failure actions can only be set for Win32 services, not for drivers.
        /// </summary>
        CANNOT_DETECT_DRIVER_FAILURE = 1080,

        /// <summary>
        /// This service runs in the same process as the service control manager.
        /// Therefore, the service control manager cannot take action if this service's process terminates unexpectedly.
        /// </summary>
        CANNOT_DETECT_PROCESS_ABORT = 1081,

        /// <summary>
        /// No recovery program has been configured for this service.
        /// </summary>
        NO_RECOVERY_PROGRAM = 1082,

        /// <summary>
        /// The executable program that this service is configured to run in does not implement the service.
        /// </summary>
        SERVICE_NOT_IN_EXE = 1083,

        /// <summary>
        /// This service cannot be started in Safe Mode
        /// </summary>
        NOT_SAFEBOOT_SERVICE = 1084,

        /// <summary>
        /// The physical end of the tape has been reached.
        /// </summary>
        END_OF_MEDIA = 1100,

        /// <summary>
        /// A tape access reached a filemark.
        /// </summary>
        FILEMARK_DETECTED = 1101,

        /// <summary>
        /// The beginning of the tape or a partition was encountered.
        /// </summary>
        BEGINNING_OF_MEDIA = 1102,

        /// <summary>
        /// A tape access reached the end of a set of files.
        /// </summary>
        SETMARK_DETECTED = 1103,

        /// <summary>
        /// No more data is on the tape.
        /// </summary>
        NO_DATA_DETECTED = 1104,

        /// <summary>
        /// Tape could not be partitioned.
        /// </summary>
        PARTITION_FAILURE = 1105,

        /// <summary>
        /// When accessing a new tape of a multivolume partition, the current block size is incorrect.
        /// </summary>
        INVALID_BLOCK_LENGTH = 1106,

        /// <summary>
        /// Tape partition information could not be found when loading a tape.
        /// </summary>
        DEVICE_NOT_PARTITIONED = 1107,

        /// <summary>
        /// Unable to lock the media eject mechanism.
        /// </summary>
        UNABLE_TO_LOCK_MEDIA = 1108,

        /// <summary>
        /// Unable to unload the media.
        /// </summary>
        UNABLE_TO_UNLOAD_MEDIA = 1109,

        /// <summary>
        /// The media in the drive may have changed.
        /// </summary>
        MEDIA_CHANGED = 1110,

        /// <summary>
        /// The I/O bus was reset.
        /// </summary>
        BUS_RESET = 1111,

        /// <summary>
        /// No media in drive.
        /// </summary>
        NO_MEDIA_IN_DRIVE = 1112,

        /// <summary>
        /// No mapping for the Unicode character exists in the target multi-byte code page.
        /// </summary>
        NO_UNICODE_TRANSLATION = 1113,

        /// <summary>
        /// A dynamic link library (DLL) initialization routine failed.
        /// </summary>
        DLL_INIT_FAILED = 1114,

        /// <summary>
        /// A system shutdown is in progress.
        /// </summary>
        SHUTDOWN_IN_PROGRESS = 1115,

        /// <summary>
        /// Unable to abort the system shutdown because no shutdown was in progress.
        /// </summary>
        NO_SHUTDOWN_IN_PROGRESS = 1116,

        /// <summary>
        /// The request could not be performed because of an I/O device error.
        /// </summary>
        IO_DEVICE = 1117,

        /// <summary>
        /// No serial device was successfully initialized. The serial driver will unload.
        /// </summary>
        SERIAL_NO_DEVICE = 1118,

        /// <summary>
        /// Unable to open a device that was sharing an interrupt request (IRQ) with other devices. At least one other device that uses that IRQ was already opened.
        /// </summary>
        IRQ_BUSY = 1119,

        /// <summary>
        /// A serial I/O operation was completed by another write to the serial port.
        /// (The IOCTL_SERIAL_XOFF_COUNTER reached zero.)
        /// </summary>
        MORE_WRITES = 1120,

        /// <summary>
        /// A serial I/O operation completed because the timeout period expired.
        /// (The IOCTL_SERIAL_XOFF_COUNTER did not reach zero.)
        /// </summary>
        COUNTER_TIMEOUT = 1121,

        /// <summary>
        /// No ID address mark was found on the floppy disk.
        /// </summary>
        FLOPPY_ID_MARK_NOT_FOUND = 1122,

        /// <summary>
        /// Mismatch between the floppy disk sector ID field and the floppy disk controller track address.
        /// </summary>
        FLOPPY_WRONG_CYLINDER = 1123,

        /// <summary>
        /// The floppy disk controller reported an error that is not recognized by the floppy disk driver.
        /// </summary>
        FLOPPY_UNKNOWN_ERROR = 1124,

        /// <summary>
        /// The floppy disk controller returned inconsistent results in its registers.
        /// </summary>
        FLOPPY_BAD_REGISTERS = 1125,

        /// <summary>
        /// While accessing the hard disk, a recalibrate operation failed, even after retries.
        /// </summary>
        DISK_RECALIBRATE_FAILED = 1126,

        /// <summary>
        /// While accessing the hard disk, a disk operation failed even after retries.
        /// </summary>
        DISK_OPERATION_FAILED = 1127,

        /// <summary>
        /// While accessing the hard disk, a disk controller reset was needed, but even that failed.
        /// </summary>
        DISK_RESET_FAILED = 1128,

        /// <summary>
        /// Physical end of tape encountered.
        /// </summary>
        EOM_OVERFLOW = 1129,

        /// <summary>
        /// Not enough server storage is available to process this command.
        /// </summary>
        NOT_ENOUGH_SERVER_MEMORY = 1130,

        /// <summary>
        /// A potential deadlock condition has been detected.
        /// </summary>
        POSSIBLE_DEADLOCK = 1131,

        /// <summary>
        /// The base address or the file offset specified does not have the proper alignment.
        /// </summary>
        MAPPED_ALIGNMENT = 1132,

        /// <summary>
        /// An attempt to change the system power state was vetoed by another application or driver.
        /// </summary>
        SET_POWER_STATE_VETOED = 1140,

        /// <summary>
        /// The system BIOS failed an attempt to change the system power state.
        /// </summary>
        SET_POWER_STATE_FAILED = 1141,

        /// <summary>
        /// An attempt was made to create more links on a file than the file system supports.
        /// </summary>
        TOO_MANY_LINKS = 1142,

        /// <summary>
        /// The specified program requires a newer version of Windows.
        /// </summary>
        OLD_WIN_VERSION = 1150,

        /// <summary>
        /// The specified program is not a Windows or MS-DOS program.
        /// </summary>
        APP_WRONG_OS = 1151,

        /// <summary>
        /// Cannot start more than one instance of the specified program.
        /// </summary>
        SINGLE_INSTANCE_APP = 1152,

        /// <summary>
        /// The specified program was written for an earlier version of Windows.
        /// </summary>
        RMODE_APP = 1153,

        /// <summary>
        /// One of the library files needed to run this application is damaged.
        /// </summary>
        INVALID_DLL = 1154,

        /// <summary>
        /// No application is associated with the specified file for this operation.
        /// </summary>
        NO_ASSOCIATION = 1155,

        /// <summary>
        /// An error occurred in sending the command to the application.
        /// </summary>
        DDE_FAIL = 1156,

        /// <summary>
        /// One of the library files needed to run this application cannot be found.
        /// </summary>
        DLL_NOT_FOUND = 1157,

        /// <summary>
        /// The current process has used all of its system allowance of handles for Window Manager objects.
        /// </summary>
        NO_MORE_USER_HANDLES = 1158,

        /// <summary>
        /// The message can be used only with synchronous operations.
        /// </summary>
        MESSAGE_SYNC_ONLY = 1159,

        /// <summary>
        /// The indicated source element has no media.
        /// </summary>
        SOURCE_ELEMENT_EMPTY = 1160,

        /// <summary>
        /// The indicated destination element already contains media.
        /// </summary>
        DESTINATION_ELEMENT_FULL = 1161,

        /// <summary>
        /// The indicated element does not exist.
        /// </summary>
        ILLEGAL_ELEMENT_ADDRESS = 1162,

        /// <summary>
        /// The indicated element is part of a magazine that is not present.
        /// </summary>
        MAGAZINE_NOT_PRESENT = 1163,

        /// <summary>
        /// The indicated device requires reinitialization due to hardware errors.
        /// </summary>
        DEVICE_REINITIALIZATION_NEEDED = 1164,

        /// <summary>
        /// The device has indicated that cleaning is required before further operations are attempted.
        /// </summary>
        DEVICE_REQUIRES_CLEANING = 1165,

        /// <summary>
        /// The device has indicated that its door is open.
        /// </summary>
        DEVICE_DOOR_OPEN = 1166,

        /// <summary>
        /// The device is not connected.
        /// </summary>
        DEVICE_NOT_CONNECTED = 1167,

        /// <summary>
        /// Element not found.
        /// </summary>
        NOT_FOUND = 1168,

        /// <summary>
        /// There was no match for the specified key in the index.
        /// </summary>
        NO_MATCH = 1169,

        /// <summary>
        /// The property set specified does not exist on the object.
        /// </summary>
        SET_NOT_FOUND = 1170,

        /// <summary>
        /// The point passed to GetMouseMovePoints is not in the buffer.
        /// </summary>
        POINT_NOT_FOUND = 1171,

        /// <summary>
        /// The tracking (workstation) service is not running.
        /// </summary>
        NO_TRACKING_SERVICE = 1172,

        /// <summary>
        /// The Volume ID could not be found.
        /// </summary>
        NO_VOLUME_ID = 1173,

        /// <summary>
        /// Unable to remove the file to be replaced.
        /// </summary>
        UNABLE_TO_REMOVE_REPLACED = 1175,

        /// <summary>
        /// Unable to move the replacement file to the file to be replaced. The file to be replaced has retained its original name.
        /// </summary>
        UNABLE_TO_MOVE_REPLACEMENT = 1176,

        /// <summary>
        /// Unable to move the replacement file to the file to be replaced. The file to be replaced has been renamed using the backup name.
        /// </summary>
        UNABLE_TO_MOVE_REPLACEMENT_2 = 1177,

        /// <summary>
        /// The volume change journal is being deleted.
        /// </summary>
        JOURNAL_DELETE_IN_PROGRESS = 1178,

        /// <summary>
        /// The volume change journal is not active.
        /// </summary>
        JOURNAL_NOT_ACTIVE = 1179,

        /// <summary>
        /// A file was found, but it may not be the correct file.
        /// </summary>
        POTENTIAL_FILE_FOUND = 1180,

        /// <summary>
        /// The journal entry has been deleted from the journal.
        /// </summary>
        JOURNAL_ENTRY_DELETED = 1181,

        /// <summary>
        /// The specified device name is invalid.
        /// </summary>
        BAD_DEVICE = 1200,

        /// <summary>
        /// The device is not currently connected but it is a remembered connection.
        /// </summary>
        CONNECTION_UNAVAIL = 1201,

        /// <summary>
        /// The local device name has a remembered connection to another network resource.
        /// </summary>
        DEVICE_ALREADY_REMEMBERED = 1202,

        /// <summary>
        /// No network provider accepted the given network path.
        /// </summary>
        NO_NET_OR_BAD_PATH = 1203,

        /// <summary>
        /// The specified network provider name is invalid.
        /// </summary>
        BAD_PROVIDER = 1204,

        /// <summary>
        /// Unable to open the network connection profile.
        /// </summary>
        CANNOT_OPEN_PROFILE = 1205,

        /// <summary>
        /// The network connection profile is corrupted.
        /// </summary>
        BAD_PROFILE = 1206,

        /// <summary>
        /// Cannot enumerate a noncontainer.
        /// </summary>
        NOT_CONTAINER = 1207,

        /// <summary>
        /// An extended error has occurred.
        /// </summary>
        EXTENDED_ERROR = 1208,

        /// <summary>
        /// The format of the specified group name is invalid.
        /// </summary>
        INVALID_GROUPNAME = 1209,

        /// <summary>
        /// The format of the specified computer name is invalid.
        /// </summary>
        INVALID_COMPUTERNAME = 1210,

        /// <summary>
        /// The format of the specified event name is invalid.
        /// </summary>
        INVALID_EVENTNAME = 1211,

        /// <summary>
        /// The format of the specified domain name is invalid.
        /// </summary>
        INVALID_DOMAINNAME = 1212,

        /// <summary>
        /// The format of the specified service name is invalid.
        /// </summary>
        INVALID_SERVICENAME = 1213,

        /// <summary>
        /// The format of the specified network name is invalid.
        /// </summary>
        INVALID_NETNAME = 1214,

        /// <summary>
        /// The format of the specified share name is invalid.
        /// </summary>
        INVALID_SHARENAME = 1215,

        /// <summary>
        /// The format of the specified password is invalid.
        /// </summary>
        INVALID_PASSWORDNAME = 1216,

        /// <summary>
        /// The format of the specified message name is invalid.
        /// </summary>
        INVALID_MESSAGENAME = 1217,

        /// <summary>
        /// The format of the specified message destination is invalid.
        /// </summary>
        INVALID_MESSAGEDEST = 1218,

        /// <summary>
        /// Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again..
        /// </summary>
        SESSION_CREDENTIAL_CONFLICT = 1219,

        /// <summary>
        /// An attempt was made to establish a session to a network server, but there are already too many sessions established to that server.
        /// </summary>
        REMOTE_SESSION_LIMIT_EXCEEDED = 1220,

        /// <summary>
        /// The workgroup or domain name is already in use by another computer on the network.
        /// </summary>
        DUP_DOMAINNAME = 1221,

        /// <summary>
        /// The network is not present or not started.
        /// </summary>
        NO_NETWORK = 1222,

        /// <summary>
        /// The operation was canceled by the user.
        /// </summary>
        CANCELLED = 1223,

        /// <summary>
        /// The requested operation cannot be performed on a file with a user-mapped section open.
        /// </summary>
        USER_MAPPED_FILE = 1224,

        /// <summary>
        /// The remote system refused the network connection.
        /// </summary>
        CONNECTION_REFUSED = 1225,

        /// <summary>
        /// The network connection was gracefully closed.
        /// </summary>
        GRACEFUL_DISCONNECT = 1226,

        /// <summary>
        /// The network transport endpoint already has an address associated with it.
        /// </summary>
        ADDRESS_ALREADY_ASSOCIATED = 1227,

        /// <summary>
        /// An address has not yet been associated with the network endpoint.
        /// </summary>
        ADDRESS_NOT_ASSOCIATED = 1228,

        /// <summary>
        /// An operation was attempted on a nonexistent network connection.
        /// </summary>
        CONNECTION_INVALID = 1229,

        /// <summary>
        /// An invalid operation was attempted on an active network connection.
        /// </summary>
        CONNECTION_ACTIVE = 1230,

        /// <summary>
        /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
        /// </summary>
        NETWORK_UNREACHABLE = 1231,

        /// <summary>
        /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
        /// </summary>
        HOST_UNREACHABLE = 1232,

        /// <summary>
        /// The network location cannot be reached. For information about network troubleshooting, see Windows Help.
        /// </summary>
        PROTOCOL_UNREACHABLE = 1233,

        /// <summary>
        /// No service is operating at the destination network endpoint on the remote system.
        /// </summary>
        PORT_UNREACHABLE = 1234,

        /// <summary>
        /// The request was aborted.
        /// </summary>
        REQUEST_ABORTED = 1235,

        /// <summary>
        /// The network connection was aborted by the local system.
        /// </summary>
        CONNECTION_ABORTED = 1236,

        /// <summary>
        /// The operation could not be completed. A retry should be performed.
        /// </summary>
        RETRY = 1237,

        /// <summary>
        /// A connection to the server could not be made because the limit on the number of concurrent connections for this account has been reached.
        /// </summary>
        CONNECTION_COUNT_LIMIT = 1238,

        /// <summary>
        /// Attempting to log in during an unauthorized time of day for this account.
        /// </summary>
        LOGIN_TIME_RESTRICTION = 1239,

        /// <summary>
        /// The account is not authorized to log in from this station.
        /// </summary>
        LOGIN_WKSTA_RESTRICTION = 1240,

        /// <summary>
        /// The network address could not be used for the operation requested.
        /// </summary>
        INCORRECT_ADDRESS = 1241,

        /// <summary>
        /// The service is already registered.
        /// </summary>
        ALREADY_REGISTERED = 1242,

        /// <summary>
        /// The specified service does not exist.
        /// </summary>
        SERVICE_NOT_FOUND = 1243,

        /// <summary>
        /// The operation being requested was not performed because the user has not been authenticated.
        /// </summary>
        NOT_AUTHENTICATED = 1244,

        /// <summary>
        /// The operation being requested was not performed because the user has not logged on to the network.
        /// The specified service does not exist.
        /// </summary>
        NOT_LOGGED_ON = 1245,

        /// <summary>
        /// Continue with work in progress.
        /// </summary>
        CONTINUE = 1246,

        /// <summary>
        /// An attempt was made to perform an initialization operation when initialization has already been completed.
        /// </summary>
        ALREADY_INITIALIZED = 1247,

        /// <summary>
        /// No more local devices.
        /// </summary>
        NO_MORE_DEVICES = 1248,

        /// <summary>
        /// The specified site does not exist.
        /// </summary>
        NO_SUCH_SITE = 1249,

        /// <summary>
        /// A domain controller with the specified name already exists.
        /// </summary>
        DOMAIN_CONTROLLER_EXISTS = 1250,

        /// <summary>
        /// This operation is supported only when you are connected to the server.
        /// </summary>
        ONLY_IF_CONNECTED = 1251,

        /// <summary>
        /// The group policy framework should call the extension even if there are no changes.
        /// </summary>
        OVERRIDE_NOCHANGES = 1252,

        /// <summary>
        /// The specified user does not have a valid profile.
        /// </summary>
        BAD_USER_PROFILE = 1253,

        /// <summary>
        /// This operation is not supported on a Microsoft Small Business Server
        /// </summary>
        NOT_SUPPORTED_ON_SBS = 1254,

        /// <summary>
        /// The server machine is shutting down.
        /// </summary>
        SERVER_SHUTDOWN_IN_PROGRESS = 1255,

        /// <summary>
        /// The remote system is not available. For information about network troubleshooting, see Windows Help.
        /// </summary>
        HOST_DOWN = 1256,

        /// <summary>
        /// The security identifier provided is not from an account domain.
        /// </summary>
        NON_ACCOUNT_SID = 1257,

        /// <summary>
        /// The security identifier provided does not have a domain component.
        /// </summary>
        NON_DOMAIN_SID = 1258,

        /// <summary>
        /// AppHelp dialog canceled thus preventing the application from starting.
        /// </summary>
        APPHELP_BLOCK = 1259,

        /// <summary>
        /// Windows cannot open this program because it has been prevented by a software restriction policy. For more information, open Event Viewer or contact your system administrator.
        /// </summary>
        ACCESS_DISABLED_BY_POLICY = 1260,

        /// <summary>
        /// A program attempt to use an invalid register value.  Normally caused by an uninitialized register. This error is Itanium specific.
        /// </summary>
        REG_NAT_CONSUMPTION = 1261,

        /// <summary>
        /// The share is currently offline or does not exist.
        /// </summary>
        CSCSHARE_OFFLINE = 1262,

        /// <summary>
        /// The kerberos protocol encountered an error while validating the
        /// KDC certificate during smartcard logon.
        /// </summary>
        PKINIT_FAILURE = 1263,

        /// <summary>
        /// The kerberos protocol encountered an error while attempting to utilize
        /// the smartcard subsystem.
        /// </summary>
        SMARTCARD_SUBSYSTEM_FAILURE = 1264,

        /// <summary>
        /// The system detected a possible attempt to compromise security. Please ensure that you can contact the server that authenticated you.
        /// </summary>
        DOWNGRADE_DETECTED = 1265,

        /// <summary>
        /// The machine is locked and can not be shut down without the force option.
        /// </summary>
        MACHINE_LOCKED = 1271,

        /// <summary>
        /// An application-defined callback gave invalid data when called.
        /// </summary>
        CALLBACK_SUPPLIED_INVALID_DATA = 1273,

        /// <summary>
        /// The group policy framework should call the extension in the synchronous foreground policy refresh.
        /// </summary>
        SYNC_FOREGROUND_REFRESH_REQUIRED = 1274,

        /// <summary>
        /// This driver has been blocked from loading
        /// </summary>
        DRIVER_BLOCKED = 1275,

        /// <summary>
        /// A dynamic link library (DLL) referenced a module that was neither a DLL nor the process's executable image.
        /// </summary>
        INVALID_IMPORT_OF_NON_DLL = 1276,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ACCESS_DISABLED_WEBBLADE = 1277,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ACCESS_DISABLED_WEBBLADE_TAMPER = 1278,

        /// <summary>
        /// No information avialable.
        /// </summary>
        RECOVERY_FAILURE = 1279,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ALREADY_FIBER = 1280,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ALREADY_THREAD = 1281,

        /// <summary>
        /// No information avialable.
        /// </summary>
        STACK_BUFFER_OVERRUN = 1282,

        /// <summary>
        /// No information avialable.
        /// </summary>
        PARAMETER_QUOTA_EXCEEDED = 1283,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DEBUGGER_INACTIVE = 1284,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DELAY_LOAD_FAILED = 1285,

        /// <summary>
        /// No information avialable.
        /// </summary>
        VDM_DISALLOWED = 1286,

        /// <summary>
        /// Not all privileges referenced are assigned to the caller.
        /// </summary>
        NOT_ALL_ASSIGNED = 1300,

        /// <summary>
        /// Some mapping between account names and security IDs was not done.
        /// </summary>
        SOME_NOT_MAPPED = 1301,

        /// <summary>
        /// No system quota limits are specifically set for this account.
        /// </summary>
        NO_QUOTAS_FOR_ACCOUNT = 1302,

        /// <summary>
        /// No encryption key is available. A well-known encryption key was returned.
        /// </summary>
        LOCAL_USER_SESSION_KEY = 1303,

        /// <summary>
        /// The password is too complex to be converted to a LAN Manager password. The LAN Manager password returned is a NULL string.
        /// </summary>
        NULL_LM_PASSWORD = 1304,

        /// <summary>
        /// The revision level is unknown.
        /// </summary>
        UNKNOWN_REVISION = 1305,

        /// <summary>
        /// Indicates two revision levels are incompatible.
        /// </summary>
        REVISION_MISMATCH = 1306,

        /// <summary>
        /// This security ID may not be assigned as the owner of this object.
        /// </summary>
        INVALID_OWNER = 1307,

        /// <summary>
        /// This security ID may not be assigned as the primary group of an object.
        /// </summary>
        INVALID_PRIMARY_GROUP = 1308,

        /// <summary>
        /// An attempt has been made to operate on an impersonation token by a thread that is not currently impersonating a client.
        /// </summary>
        NO_IMPERSONATION_TOKEN = 1309,

        /// <summary>
        /// The group may not be disabled.
        /// </summary>
        CANT_DISABLE_MANDATORY = 1310,

        /// <summary>
        /// There are currently no logon servers available to service the logon request.
        /// </summary>
        NO_LOGON_SERVERS = 1311,

        /// <summary>
        /// A specified logon session does not exist. It may already have been terminated.
        /// </summary>
        NO_SUCH_LOGON_SESSION = 1312,

        /// <summary>
        /// A specified privilege does not exist.
        /// </summary>
        NO_SUCH_PRIVILEGE = 1313,

        /// <summary>
        /// A required privilege is not held by the client.
        /// </summary>
        PRIVILEGE_NOT_HELD = 1314,

        /// <summary>
        /// The name provided is not a properly formed account name.
        /// </summary>
        INVALID_ACCOUNT_NAME = 1315,

        /// <summary>
        /// The specified user already exists.
        /// </summary>
        USER_EXISTS = 1316,

        /// <summary>
        /// The specified user does not exist.
        /// </summary>
        NO_SUCH_USER = 1317,

        /// <summary>
        /// The specified group already exists.
        /// </summary>
        GROUP_EXISTS = 1318,

        /// <summary>
        /// The specified group does not exist.
        /// </summary>
        NO_SUCH_GROUP = 1319,

        /// <summary>
        /// Either the specified user account is already a member of the specified group, or the specified group cannot be deleted because it contains a member.
        /// </summary>
        MEMBER_IN_GROUP = 1320,

        /// <summary>
        /// The specified user account is not a member of the specified group account.
        /// </summary>
        MEMBER_NOT_IN_GROUP = 1321,

        /// <summary>
        /// The last remaining administration account cannot be disabled or deleted.
        /// </summary>
        LAST_ADMIN = 1322,

        /// <summary>
        /// Unable to update the password. The value provided as the current password is incorrect.
        /// </summary>
        WRONG_PASSWORD = 1323,

        /// <summary>
        /// Unable to update the password. The value provided for the new password contains values that are not allowed in passwords.
        /// </summary>
        ILL_FORMED_PASSWORD = 1324,

        /// <summary>
        /// Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirement of the domain.
        /// </summary>
        PASSWORD_RESTRICTION = 1325,

        /// <summary>
        /// Logon failure: unknown user name or bad password.
        /// </summary>
        LOGON_FAILURE = 1326,

        /// <summary>
        /// Logon failure: user account restriction.  Possible reasons are blank passwords not allowed, logon hour restrictions, or a policy restriction has been enforced.
        /// </summary>
        ACCOUNT_RESTRICTION = 1327,

        /// <summary>
        /// Logon failure: account logon time restriction violation.
        /// </summary>
        INVALID_LOGON_HOURS = 1328,

        /// <summary>
        /// Logon failure: user not allowed to log on to this computer.
        /// </summary>
        INVALID_WORKSTATION = 1329,

        /// <summary>
        /// Logon failure: the specified account password has expired.
        /// </summary>
        PASSWORD_EXPIRED = 1330,

        /// <summary>
        /// Logon failure: account currently disabled.
        /// </summary>
        ACCOUNT_DISABLED = 1331,

        /// <summary>
        /// No mapping between account names and security IDs was done.
        /// </summary>
        NONE_MAPPED = 1332,

        /// <summary>
        /// Too many local user identifiers (LUIDs) were requested at one time.
        /// </summary>
        TOO_MANY_LUIDS_REQUESTED = 1333,

        /// <summary>
        /// No more local user identifiers (LUIDs) are available.
        /// </summary>
        LUIDS_EXHAUSTED = 1334,

        /// <summary>
        /// The subauthority part of a security ID is invalid for this particular use.
        /// </summary>
        INVALID_SUB_AUTHORITY = 1335,

        /// <summary>
        /// The access control list (ACL) structure is invalid.
        /// </summary>
        INVALID_ACL = 1336,

        /// <summary>
        /// The security ID structure is invalid.
        /// </summary>
        INVALID_SID = 1337,

        /// <summary>
        /// The security descriptor structure is invalid.
        /// </summary>
        INVALID_SECURITY_DESCR = 1338,

        /// <summary>
        /// The inherited access control list (ACL) or access control entry (ACE) could not be built.
        /// </summary>
        BAD_INHERITANCE_ACL = 1340,

        /// <summary>
        /// The server is currently disabled.
        /// </summary>
        SERVER_DISABLED = 1341,

        /// <summary>
        /// The server is currently enabled.
        /// </summary>
        SERVER_NOT_DISABLED = 1342,

        /// <summary>
        /// The value provided was an invalid value for an identifier authority.
        /// </summary>
        INVALID_ID_AUTHORITY = 1343,

        /// <summary>
        /// No more memory is available for security information updates.
        /// </summary>
        ALLOTTED_SPACE_EXCEEDED = 1344,

        /// <summary>
        /// The specified attributes are invalid, or incompatible with the attributes for the group as a whole.
        /// </summary>
        INVALID_GROUP_ATTRIBUTES = 1345,

        /// <summary>
        /// Either a required impersonation level was not provided, or the provided impersonation level is invalid.
        /// </summary>
        BAD_IMPERSONATION_LEVEL = 1346,

        /// <summary>
        /// Cannot open an anonymous level security token.
        /// </summary>
        CANT_OPEN_ANONYMOUS = 1347,

        /// <summary>
        /// The validation information class requested was invalid.
        /// </summary>
        BAD_VALIDATION_CLASS = 1348,

        /// <summary>
        /// The type of the token is inappropriate for its attempted use.
        /// </summary>
        BAD_TOKEN_TYPE = 1349,

        /// <summary>
        /// Unable to perform a security operation on an object that has no associated security.
        /// </summary>
        NO_SECURITY_ON_OBJECT = 1350,

        /// <summary>
        /// Configuration information could not be read from the domain controller, either because the machine is unavailable, or access has been denied.
        /// </summary>
        CANT_ACCESS_DOMAIN_INFO = 1351,

        /// <summary>
        /// The security account manager (SAM) or local security authority (LSA) server was in the wrong state to perform the security operation.
        /// </summary>
        INVALID_SERVER_STATE = 1352,

        /// <summary>
        /// The domain was in the wrong state to perform the security operation.
        /// </summary>
        INVALID_DOMAIN_STATE = 1353,

        /// <summary>
        /// This operation is only allowed for the Primary Domain Controller of the domain.
        /// </summary>
        INVALID_DOMAIN_ROLE = 1354,

        /// <summary>
        /// The specified domain either does not exist or could not be contacted.
        /// </summary>
        NO_SUCH_DOMAIN = 1355,

        /// <summary>
        /// The specified domain already exists.
        /// </summary>
        DOMAIN_EXISTS = 1356,

        /// <summary>
        /// An attempt was made to exceed the limit on the number of domains per server.
        /// </summary>
        DOMAIN_LIMIT_EXCEEDED = 1357,

        /// <summary>
        /// Unable to complete the requested operation because of either a catastrophic media failure or a data structure corruption on the disk.
        /// </summary>
        INTERNAL_DB_CORRUPTION = 1358,

        /// <summary>
        /// An internal error occurred.
        /// </summary>
        INTERNAL_ERROR = 1359,

        /// <summary>
        /// Generic access types were contained in an access mask which should already be mapped to nongeneric types.
        /// </summary>
        GENERIC_NOT_MAPPED = 1360,

        /// <summary>
        /// A security descriptor is not in the right format (absolute or self-relative).
        /// </summary>
        BAD_DESCRIPTOR_FORMAT = 1361,

        /// <summary>
        /// The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process.
        /// </summary>
        NOT_LOGON_PROCESS = 1362,

        /// <summary>
        /// Cannot start a new logon session with an ID that is already in use.
        /// </summary>
        LOGON_SESSION_EXISTS = 1363,

        /// <summary>
        /// A specified authentication package is unknown.
        /// </summary>
        NO_SUCH_PACKAGE = 1364,

        /// <summary>
        /// The logon session is not in a state that is consistent with the requested operation.
        /// </summary>
        BAD_LOGON_SESSION_STATE = 1365,

        /// <summary>
        /// The logon session ID is already in use.
        /// </summary>
        LOGON_SESSION_COLLISION = 1366,

        /// <summary>
        /// A logon request contained an invalid logon type value.
        /// </summary>
        INVALID_LOGON_TYPE = 1367,

        /// <summary>
        /// Unable to impersonate using a named pipe until data has been read from that pipe.
        /// </summary>
        CANNOT_IMPERSONATE = 1368,

        /// <summary>
        /// The transaction state of a registry subtree is incompatible with the requested operation.
        /// </summary>
        RXACT_INVALID_STATE = 1369,

        /// <summary>
        /// An internal security database corruption has been encountered.
        /// </summary>
        RXACT_COMMIT_FAILURE = 1370,

        /// <summary>
        /// Cannot perform this operation on built-in accounts.
        /// </summary>
        SPECIAL_ACCOUNT = 1371,

        /// <summary>
        /// Cannot perform this operation on this built-in special group.
        /// </summary>
        SPECIAL_GROUP = 1372,

        /// <summary>
        /// Cannot perform this operation on this built-in special user.
        /// </summary>
        SPECIAL_USER = 1373,

        /// <summary>
        /// The user cannot be removed from a group because the group is currently the user's primary group.
        /// </summary>
        MEMBERS_PRIMARY_GROUP = 1374,

        /// <summary>
        /// The token is already in use as a primary token.
        /// </summary>
        TOKEN_ALREADY_IN_USE = 1375,

        /// <summary>
        /// The specified local group does not exist.
        /// </summary>
        NO_SUCH_ALIAS = 1376,

        /// <summary>
        /// The specified account name is not a member of the local group.
        /// </summary>
        MEMBER_NOT_IN_ALIAS = 1377,

        /// <summary>
        /// The specified account name is already a member of the local group.
        /// </summary>
        MEMBER_IN_ALIAS = 1378,

        /// <summary>
        /// The specified local group already exists.
        /// </summary>
        ALIAS_EXISTS = 1379,

        /// <summary>
        /// Logon failure: the user has not been granted the requested logon type at this computer.
        /// </summary>
        LOGON_NOT_GRANTED = 1380,

        /// <summary>
        /// The maximum number of secrets that may be stored in a single system has been exceeded.
        /// </summary>
        TOO_MANY_SECRETS = 1381,

        /// <summary>
        /// The length of a secret exceeds the maximum length allowed.
        /// </summary>
        SECRET_TOO_LONG = 1382,

        /// <summary>
        /// The local security authority database contains an internal inconsistency.
        /// </summary>
        INTERNAL_DB_ERROR = 1383,

        /// <summary>
        /// During a logon attempt, the user's security context accumulated too many security IDs.
        /// </summary>
        TOO_MANY_CONTEXT_IDS = 1384,

        /// <summary>
        /// Logon failure: the user has not been granted the requested logon type at this computer.
        /// </summary>
        LOGON_TYPE_NOT_GRANTED = 1385,

        /// <summary>
        /// A cross-encrypted password is necessary to change a user password.
        /// </summary>
        NT_CROSS_ENCRYPTION_REQUIRED = 1386,

        /// <summary>
        /// A member could not be added to or removed from the local group because the member does not exist.
        /// </summary>
        NO_SUCH_MEMBER = 1387,

        /// <summary>
        /// A new member could not be added to a local group because the member has the wrong account type.
        /// </summary>
        INVALID_MEMBER = 1388,

        /// <summary>
        /// Too many security IDs have been specified.
        /// </summary>
        TOO_MANY_SIDS = 1389,

        /// <summary>
        /// A cross-encrypted password is necessary to change this user password.
        /// </summary>
        LM_CROSS_ENCRYPTION_REQUIRED = 1390,

        /// <summary>
        /// Indicates an ACL contains no inheritable components.
        /// </summary>
        NO_INHERITANCE = 1391,

        /// <summary>
        /// The file or directory is corrupted and unreadable.
        /// </summary>
        FILE_CORRUPT = 1392,

        /// <summary>
        /// The disk structure is corrupted and unreadable.
        /// </summary>
        DISK_CORRUPT = 1393,

        /// <summary>
        /// There is no user session key for the specified logon session.
        /// </summary>
        NO_USER_SESSION_KEY = 1394,

        /// <summary>
        /// The service being accessed is licensed for a particular number of connections.
        /// No more connections can be made to the service at this time because there are already as many connections as the service can accept.
        /// </summary>
        LICENSE_QUOTA_EXCEEDED = 1395,

        /// <summary>
        /// Logon Failure: The target account name is incorrect.
        /// </summary>
        WRONG_TARGET_NAME = 1396,

        /// <summary>
        /// Mutual Authentication failed. The server's password is out of date at the domain controller.
        /// </summary>
        MUTUAL_AUTH_FAILED = 1397,

        /// <summary>
        /// There is a time and/or date difference between the client and server.
        /// </summary>
        TIME_SKEW = 1398,

        /// <summary>
        /// This operation can not be performed on the current domain.
        /// </summary>
        CURRENT_DOMAIN_NOT_ALLOWED = 1399,

        /// <summary>
        /// Invalid window handle.
        /// </summary>
        INVALID_WINDOW_HANDLE = 1400,

        /// <summary>
        /// Invalid menu handle.
        /// </summary>
        INVALID_MENU_HANDLE = 1401,

        /// <summary>
        /// Invalid cursor handle.
        /// </summary>
        INVALID_CURSOR_HANDLE = 1402,

        /// <summary>
        /// Invalid accelerator table handle.
        /// </summary>
        INVALID_ACCEL_HANDLE = 1403,

        /// <summary>
        /// Invalid hook handle.
        /// </summary>
        INVALID_HOOK_HANDLE = 1404,

        /// <summary>
        /// Invalid handle to a multiple-window position structure.
        /// </summary>
        INVALID_DWP_HANDLE = 1405,

        /// <summary>
        /// Cannot create a top-level child window.
        /// </summary>
        TLW_WITH_WSCHILD = 1406,

        /// <summary>
        /// Cannot find window class.
        /// </summary>
        CANNOT_FIND_WND_CLASS = 1407,

        /// <summary>
        /// Invalid window, it belongs to other thread.
        /// </summary>
        WINDOW_OF_OTHER_THREAD = 1408,

        /// <summary>
        /// Hot key is already registered.
        /// </summary>
        HOTKEY_ALREADY_REGISTERED = 1409,

        /// <summary>
        /// Class already exists.
        /// </summary>
        CLASS_ALREADY_EXISTS = 1410,

        /// <summary>
        /// Class does not exist.
        /// </summary>
        CLASS_DOES_NOT_EXIST = 1411,

        /// <summary>
        /// Class still has open windows.
        /// </summary>
        CLASS_HAS_WINDOWS = 1412,

        /// <summary>
        /// Invalid index.
        /// </summary>
        INVALID_INDEX = 1413,

        /// <summary>
        /// Invalid icon handle.
        /// </summary>
        INVALID_ICON_HANDLE = 1414,

        /// <summary>
        /// Using private DIALOG window words.
        /// </summary>
        PRIVATE_DIALOG_INDEX = 1415,

        /// <summary>
        /// The list box identifier was not found.
        /// </summary>
        LISTBOX_ID_NOT_FOUND = 1416,

        /// <summary>
        /// No wildcards were found.
        /// </summary>
        NO_WILDCARD_CHARACTERS = 1417,

        /// <summary>
        /// Thread does not have a clipboard open.
        /// </summary>
        CLIPBOARD_NOT_OPEN = 1418,

        /// <summary>
        /// Hot key is not registered.
        /// </summary>
        HOTKEY_NOT_REGISTERED = 1419,

        /// <summary>
        /// The window is not a valid dialog window.
        /// </summary>
        WINDOW_NOT_DIALOG = 1420,

        /// <summary>
        /// Control ID not found.
        /// </summary>
        CONTROL_ID_NOT_FOUND = 1421,

        /// <summary>
        /// Invalid message for a combo box because it does not have an edit control.
        /// </summary>
        INVALID_COMBOBOX_MESSAGE = 1422,

        /// <summary>
        /// The window is not a combo box.
        /// </summary>
        WINDOW_NOT_COMBOBOX = 1423,

        /// <summary>
        /// Height must be less than 256.
        /// </summary>
        INVALID_EDIT_HEIGHT = 1424,

        /// <summary>
        /// Invalid device context (DC) handle.
        /// </summary>
        DC_NOT_FOUND = 1425,

        /// <summary>
        /// Invalid hook procedure type.
        /// </summary>
        INVALID_HOOK_FILTER = 1426,

        /// <summary>
        /// Invalid hook procedure.
        /// </summary>
        INVALID_FILTER_PROC = 1427,

        /// <summary>
        /// Cannot set nonlocal hook without a module handle.
        /// </summary>
        HOOK_NEEDS_HMOD = 1428,

        /// <summary>
        /// This hook procedure can only be set globally.
        /// </summary>
        GLOBAL_ONLY_HOOK = 1429,

        /// <summary>
        /// The journal hook procedure is already installed.
        /// </summary>
        JOURNAL_HOOK_SET = 1430,

        /// <summary>
        /// The hook procedure is not installed.
        /// </summary>
        HOOK_NOT_INSTALLED = 1431,

        /// <summary>
        /// Invalid message for single-selection list box.
        /// </summary>
        INVALID_LB_MESSAGE = 1432,

        /// <summary>
        /// LB_SETCOUNT sent to non-lazy list box.
        /// </summary>
        SETCOUNT_ON_BAD_LB = 1433,

        /// <summary>
        /// This list box does not support tab stops.
        /// </summary>
        LB_WITHOUT_TABSTOPS = 1434,

        /// <summary>
        /// Cannot destroy object created by another thread.
        /// </summary>
        DESTROY_OBJECT_OF_OTHER_THREAD = 1435,

        /// <summary>
        /// Child windows cannot have menus.
        /// </summary>
        CHILD_WINDOW_MENU = 1436,

        /// <summary>
        /// The window does not have a system menu.
        /// </summary>
        NO_SYSTEM_MENU = 1437,

        /// <summary>
        /// Invalid message box style.
        /// </summary>
        INVALID_MSGBOX_STYLE = 1438,

        /// <summary>
        /// Invalid system-wide (SPI_*) parameter.
        /// </summary>
        INVALID_SPI_VALUE = 1439,

        /// <summary>
        /// Screen already locked.
        /// </summary>
        SCREEN_ALREADY_LOCKED = 1440,

        /// <summary>
        /// All handles to windows in a multiple-window position structure must have the same parent.
        /// </summary>
        HWNDS_HAVE_DIFF_PARENT = 1441,

        /// <summary>
        /// The window is not a child window.
        /// </summary>
        NOT_CHILD_WINDOW = 1442,

        /// <summary>
        /// Invalid GW_* command.
        /// </summary>
        INVALID_GW_COMMAND = 1443,

        /// <summary>
        /// Invalid thread identifier.
        /// </summary>
        INVALID_THREAD_ID = 1444,

        /// <summary>
        /// Cannot process a message from a window that is not a multiple document interface (MDI) window.
        /// </summary>
        NON_MDICHILD_WINDOW = 1445,

        /// <summary>
        /// Popup menu already active.
        /// </summary>
        POPUP_ALREADY_ACTIVE = 1446,

        /// <summary>
        /// The window does not have scroll bars.
        /// </summary>
        NO_SCROLLBARS = 1447,

        /// <summary>
        /// Scroll bar range cannot be greater than MAXLONG.
        /// </summary>
        INVALID_SCROLLBAR_RANGE = 1448,

        /// <summary>
        /// Cannot show or remove the window in the way specified.
        /// </summary>
        INVALID_SHOWWIN_COMMAND = 1449,

        /// <summary>
        /// Insufficient system resources exist to complete the requested service.
        /// </summary>
        NO_SYSTEM_RESOURCES = 1450,

        /// <summary>
        /// Insufficient system resources exist to complete the requested service.
        /// </summary>
        NONPAGED_SYSTEM_RESOURCES = 1451,

        /// <summary>
        /// Insufficient system resources exist to complete the requested service.
        /// </summary>
        PAGED_SYSTEM_RESOURCES = 1452,

        /// <summary>
        /// Insufficient quota to complete the requested service.
        /// </summary>
        WORKING_SET_QUOTA = 1453,

        /// <summary>
        /// Insufficient quota to complete the requested service.
        /// </summary>
        PAGEFILE_QUOTA = 1454,

        /// <summary>
        /// The paging file is too small for this operation to complete.
        /// </summary>
        COMMITMENT_LIMIT = 1455,

        /// <summary>
        /// A menu item was not found.
        /// </summary>
        MENU_ITEM_NOT_FOUND = 1456,

        /// <summary>
        /// Invalid keyboard layout handle.
        /// </summary>
        INVALID_KEYBOARD_HANDLE = 1457,

        /// <summary>
        /// Hook type not allowed.
        /// </summary>
        HOOK_TYPE_NOT_ALLOWED = 1458,

        /// <summary>
        /// This operation requires an interactive window station.
        /// </summary>
        REQUIRES_INTERACTIVE_WINDOWSTATION = 1459,

        /// <summary>
        /// This operation returned because the timeout period expired.
        /// </summary>
        TIMEOUT = 1460,

        /// <summary>
        /// Invalid monitor handle.
        /// </summary>
        INVALID_MONITOR_HANDLE = 1461,

        /// <summary>
        /// The event log file is corrupted.
        /// </summary>
        EVENTLOG_FILE_CORRUPT = 1500,

        /// <summary>
        /// No event log file could be opened, so the event logging service did not start.
        /// </summary>
        EVENTLOG_CANT_START = 1501,

        /// <summary>
        /// The event log file is full.
        /// </summary>
        LOG_FILE_FULL = 1502,

        /// <summary>
        /// The event log file has changed between read operations.
        /// </summary>
        EVENTLOG_FILE_CHANGED = 1503,

        /// <summary>
        /// The Windows Installer Service could not be accessed. This can occur if you are running Windows in safe mode, or if the Windows Installer is not correctly installed. Contact your support personnel for assistance.
        /// </summary>
        INSTALL_SERVICE_FAILURE = 1601,

        /// <summary>
        /// User cancelled installation.
        /// </summary>
        INSTALL_USEREXIT = 1602,

        /// <summary>
        /// Fatal error during installation.
        /// </summary>
        INSTALL_FAILURE = 1603,

        /// <summary>
        /// Installation suspended, incomplete.
        /// </summary>
        INSTALL_SUSPEND = 1604,

        /// <summary>
        /// This action is only valid for products that are currently installed.
        /// </summary>
        UNKNOWN_PRODUCT = 1605,

        /// <summary>
        /// Feature ID not registered.
        /// </summary>
        UNKNOWN_FEATURE = 1606,

        /// <summary>
        /// Component ID not registered.
        /// </summary>
        UNKNOWN_COMPONENT = 1607,

        /// <summary>
        /// Unknown property.
        /// </summary>
        UNKNOWN_PROPERTY = 1608,

        /// <summary>
        /// Handle is in an invalid state.
        /// </summary>
        INVALID_HANDLE_STATE = 1609,

        /// <summary>
        /// The configuration data for this product is corrupt.  Contact your support personnel.
        /// </summary>
        BAD_CONFIGURATION = 1610,

        /// <summary>
        /// Component qualifier not present.
        /// </summary>
        INDEX_ABSENT = 1611,

        /// <summary>
        /// The installation source for this product is not available.  Verify that the source exists and that you can access it.
        /// </summary>
        INSTALL_SOURCE_ABSENT = 1612,

        /// <summary>
        /// This installation package cannot be installed by the Windows Installer service.  You must install a Windows service pack that contains a newer version of the Windows Installer service.
        /// </summary>
        INSTALL_PACKAGE_VERSION = 1613,

        /// <summary>
        /// Product is uninstalled.
        /// </summary>
        PRODUCT_UNINSTALLED = 1614,

        /// <summary>
        /// SQL query syntax invalid or unsupported.
        /// </summary>
        BAD_QUERY_SYNTAX = 1615,

        /// <summary>
        /// Record field does not exist.
        /// </summary>
        INVALID_FIELD = 1616,

        /// <summary>
        /// The device has been removed.
        /// </summary>
        DEVICE_REMOVED = 1617,

        /// <summary>
        /// Another installation is already in progress.  Complete that installation before proceeding with this install.
        /// </summary>
        INSTALL_ALREADY_RUNNING = 1618,

        /// <summary>
        /// This installation package could not be opened.  Verify that the package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer package.
        /// </summary>
        INSTALL_PACKAGE_OPEN_FAILED = 1619,

        /// <summary>
        /// This installation package could not be opened.  Contact the application vendor to verify that this is a valid Windows Installer package.
        /// </summary>
        INSTALL_PACKAGE_INVALID = 1620,

        /// <summary>
        /// There was an error starting the Windows Installer service user interface.  Contact your support personnel.
        /// </summary>
        INSTALL_UI_FAILURE = 1621,

        /// <summary>
        /// Error opening installation log file. Verify that the specified log file location exists and that you can write to it.
        /// </summary>
        INSTALL_LOG_FAILURE = 1622,

        /// <summary>
        /// The language of this installation package is not supported by your system.
        /// </summary>
        INSTALL_LANGUAGE_UNSUPPORTED = 1623,

        /// <summary>
        /// Error applying transforms.  Verify that the specified transform paths are valid.
        /// </summary>
        INSTALL_TRANSFORM_FAILURE = 1624,

        /// <summary>
        /// This installation is forbidden by system policy.  Contact your system administrator.
        /// </summary>
        INSTALL_PACKAGE_REJECTED = 1625,

        /// <summary>
        /// Function could not be executed.
        /// </summary>
        FUNCTION_NOT_CALLED = 1626,

        /// <summary>
        /// Function failed during execution.
        /// </summary>
        FUNCTION_FAILED = 1627,

        /// <summary>
        /// Invalid or unknown table specified.
        /// </summary>
        INVALID_TABLE = 1628,

        /// <summary>
        /// Data supplied is of wrong type.
        /// </summary>
        DATATYPE_MISMATCH = 1629,

        /// <summary>
        /// Data of this type is not supported.
        /// </summary>
        UNSUPPORTED_TYPE = 1630,

        /// <summary>
        /// The Windows Installer service failed to start.  Contact your support personnel.
        /// </summary>
        CREATE_FAILED = 1631,

        /// <summary>
        /// The Temp folder is on a drive that is full or is inaccessible. Free up space on the drive or verify that you have write permission on the Temp folder.
        /// </summary>
        INSTALL_TEMP_UNWRITABLE = 1632,

        /// <summary>
        /// This installation package is not supported by this processor type. Contact your product vendor.
        /// </summary>
        INSTALL_PLATFORM_UNSUPPORTED = 1633,

        /// <summary>
        /// Component not used on this computer.
        /// </summary>
        INSTALL_NOTUSED = 1634,

        /// <summary>
        /// This patch package could not be opened.  Verify that the patch package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer patch package.
        /// </summary>
        PATCH_PACKAGE_OPEN_FAILED = 1635,

        /// <summary>
        /// This patch package could not be opened.  Contact the application vendor to verify that this is a valid Windows Installer patch package.
        /// </summary>
        PATCH_PACKAGE_INVALID = 1636,

        /// <summary>
        /// This patch package cannot be processed by the Windows Installer service.  You must install a Windows service pack that contains a newer version of the Windows Installer service.
        /// </summary>
        PATCH_PACKAGE_UNSUPPORTED = 1637,

        /// <summary>
        /// Another version of this product is already installed.  Installation of this version cannot continue.  To configure or remove the existing version of this product, use Add/Remove Programs on the Control Panel.
        /// </summary>
        PRODUCT_VERSION = 1638,

        /// <summary>
        /// Invalid command line argument.  Consult the Windows Installer SDK for detailed command line help.
        /// </summary>
        INVALID_COMMAND_LINE = 1639,

        /// <summary>
        /// Only administrators have permission to add, remove, or configure server software during a Terminal services remote session. If you want to install or configure software on the server, contact your network administrator.
        /// </summary>
        INSTALL_REMOTE_DISALLOWED = 1640,

        /// <summary>
        /// The requested operation completed successfully.  The system will be restarted so the changes can take effect.
        /// </summary>
        SUCCESS_REBOOT_INITIATED = 1641,

        /// <summary>
        /// The upgrade patch cannot be installed by the Windows Installer service because the program to be upgraded may be missing, or the upgrade patch may update a different version of the program. Verify that the program to be upgraded exists on your computer an
        /// d that you have the correct upgrade patch.
        /// </summary>
        PATCH_TARGET_NOT_FOUND = 1642,

        /// <summary>
        /// The patch package is not permitted by software restriction policy.
        /// </summary>
        PATCH_PACKAGE_REJECTED = 1643,

        /// <summary>
        /// One or more customizations are not permitted by software restriction policy.
        /// </summary>
        INSTALL_TRANSFORM_REJECTED = 1644,

        /// <summary>
        /// No information avialable.
        /// </summary>
        INSTALL_REMOTE_PROHIBITED = 1645,

        /// <summary>
        /// The string binding is invalid.
        /// </summary>
        RPC_S_INVALID_STRING_BINDING = 1700,

        /// <summary>
        /// The binding handle is not the correct type.
        /// </summary>
        RPC_S_WRONG_KIND_OF_BINDING = 1701,

        /// <summary>
        /// The binding handle is invalid.
        /// </summary>
        RPC_S_INVALID_BINDING = 1702,

        /// <summary>
        /// The RPC protocol sequence is not supported.
        /// </summary>
        RPC_S_PROTSEQ_NOT_SUPPORTED = 1703,

        /// <summary>
        /// The RPC protocol sequence is invalid.
        /// </summary>
        RPC_S_INVALID_RPC_PROTSEQ = 1704,

        /// <summary>
        /// The string universal unique identifier (UUID) is invalid.
        /// </summary>
        RPC_S_INVALID_STRING_UUID = 1705,

        /// <summary>
        /// The endpoint format is invalid.
        /// </summary>
        RPC_S_INVALID_ENDPOINT_FORMAT = 1706,

        /// <summary>
        /// The network address is invalid.
        /// </summary>
        RPC_S_INVALID_NET_ADDR = 1707,

        /// <summary>
        /// No endpoint was found.
        /// </summary>
        RPC_S_NO_ENDPOINT_FOUND = 1708,

        /// <summary>
        /// The timeout value is invalid.
        /// </summary>
        RPC_S_INVALID_TIMEOUT = 1709,

        /// <summary>
        /// The object universal unique identifier (UUID) was not found.
        /// </summary>
        RPC_S_OBJECT_NOT_FOUND = 1710,

        /// <summary>
        /// The object universal unique identifier (UUID) has already been registered.
        /// </summary>
        RPC_S_ALREADY_REGISTERED = 1711,

        /// <summary>
        /// The type universal unique identifier (UUID) has already been registered.
        /// </summary>
        RPC_S_TYPE_ALREADY_REGISTERED = 1712,

        /// <summary>
        /// The RPC server is already listening.
        /// </summary>
        RPC_S_ALREADY_LISTENING = 1713,

        /// <summary>
        /// No protocol sequences have been registered.
        /// </summary>
        RPC_S_NO_PROTSEQS_REGISTERED = 1714,

        /// <summary>
        /// The RPC server is not listening.
        /// </summary>
        RPC_S_NOT_LISTENING = 1715,

        /// <summary>
        /// The manager type is unknown.
        /// </summary>
        RPC_S_UNKNOWN_MGR_TYPE = 1716,

        /// <summary>
        /// The interface is unknown.
        /// </summary>
        RPC_S_UNKNOWN_IF = 1717,

        /// <summary>
        /// There are no bindings.
        /// </summary>
        RPC_S_NO_BINDINGS = 1718,

        /// <summary>
        /// There are no protocol sequences.
        /// </summary>
        RPC_S_NO_PROTSEQS = 1719,

        /// <summary>
        /// The endpoint cannot be created.
        /// </summary>
        RPC_S_CANT_CREATE_ENDPOINT = 1720,

        /// <summary>
        /// Not enough resources are available to complete this operation.
        /// </summary>
        RPC_S_OUT_OF_RESOURCES = 1721,

        /// <summary>
        /// The RPC server is unavailable.
        /// </summary>
        RPC_S_SERVER_UNAVAILABLE = 1722,

        /// <summary>
        /// The RPC server is too busy to complete this operation.
        /// </summary>
        RPC_S_SERVER_TOO_BUSY = 1723,

        /// <summary>
        /// The network options are invalid.
        /// </summary>
        RPC_S_INVALID_NETWORK_OPTIONS = 1724,

        /// <summary>
        /// There are no remote procedure calls active on this thread.
        /// </summary>
        RPC_S_NO_CALL_ACTIVE = 1725,

        /// <summary>
        /// The remote procedure call failed.
        /// </summary>
        RPC_S_CALL_FAILED = 1726,

        /// <summary>
        /// The remote procedure call failed and did not execute.
        /// </summary>
        RPC_S_CALL_FAILED_DNE = 1727,

        /// <summary>
        /// A remote procedure call (RPC) protocol error occurred.
        /// </summary>
        RPC_S_PROTOCOL_ERROR = 1728,

        /// <summary>
        /// The transfer syntax is not supported by the RPC server.
        /// </summary>
        RPC_S_UNSUPPORTED_TRANS_SYN = 1730,

        /// <summary>
        /// The universal unique identifier (UUID) type is not supported.
        /// </summary>
        RPC_S_UNSUPPORTED_TYPE = 1732,

        /// <summary>
        /// The tag is invalid.
        /// </summary>
        RPC_S_INVALID_TAG = 1733,

        /// <summary>
        /// The array bounds are invalid.
        /// </summary>
        RPC_S_INVALID_BOUND = 1734,

        /// <summary>
        /// The binding does not contain an entry name.
        /// </summary>
        RPC_S_NO_ENTRY_NAME = 1735,

        /// <summary>
        /// The name syntax is invalid.
        /// </summary>
        RPC_S_INVALID_NAME_SYNTAX = 1736,

        /// <summary>
        /// The name syntax is not supported.
        /// </summary>
        RPC_S_UNSUPPORTED_NAME_SYNTAX = 1737,

        /// <summary>
        /// No network address is available to use to construct a universal unique identifier (UUID).
        /// </summary>
        RPC_S_UUID_NO_ADDRESS = 1739,

        /// <summary>
        /// The endpoint is a duplicate.
        /// </summary>
        RPC_S_DUPLICATE_ENDPOINT = 1740,

        /// <summary>
        /// The authentication type is unknown.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_TYPE = 1741,

        /// <summary>
        /// The maximum number of calls is too small.
        /// </summary>
        RPC_S_MAX_CALLS_TOO_SMALL = 1742,

        /// <summary>
        /// The string is too long.
        /// </summary>
        RPC_S_STRING_TOO_LONG = 1743,

        /// <summary>
        /// The RPC protocol sequence was not found.
        /// </summary>
        RPC_S_PROTSEQ_NOT_FOUND = 1744,

        /// <summary>
        /// The procedure number is out of range.
        /// </summary>
        RPC_S_PROCNUM_OUT_OF_RANGE = 1745,

        /// <summary>
        /// The binding does not contain any authentication information.
        /// </summary>
        RPC_S_BINDING_HAS_NO_AUTH = 1746,

        /// <summary>
        /// The authentication service is unknown.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_SERVICE = 1747,

        /// <summary>
        /// The authentication level is unknown.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_LEVEL = 1748,

        /// <summary>
        /// The security context is invalid.
        /// </summary>
        RPC_S_INVALID_AUTH_IDENTITY = 1749,

        /// <summary>
        /// The authorization service is unknown.
        /// </summary>
        RPC_S_UNKNOWN_AUTHZ_SERVICE = 1750,

        /// <summary>
        /// The entry is invalid.
        /// </summary>
        EPT_S_INVALID_ENTRY = 1751,

        /// <summary>
        /// The server endpoint cannot perform the operation.
        /// </summary>
        EPT_S_CANT_PERFORM_OP = 1752,

        /// <summary>
        /// There are no more endpoints available from the endpoint mapper.
        /// </summary>
        EPT_S_NOT_REGISTERED = 1753,

        /// <summary>
        /// No interfaces have been exported.
        /// </summary>
        RPC_S_NOTHING_TO_EXPORT = 1754,

        /// <summary>
        /// The entry name is incomplete.
        /// </summary>
        RPC_S_INCOMPLETE_NAME = 1755,

        /// <summary>
        /// The version option is invalid.
        /// </summary>
        RPC_S_INVALID_VERS_OPTION = 1756,

        /// <summary>
        /// There are no more members.
        /// </summary>
        RPC_S_NO_MORE_MEMBERS = 1757,

        /// <summary>
        /// There is nothing to unexport.
        /// </summary>
        RPC_S_NOT_ALL_OBJS_UNEXPORTED = 1758,

        /// <summary>
        /// The interface was not found.
        /// </summary>
        RPC_S_INTERFACE_NOT_FOUND = 1759,

        /// <summary>
        /// The entry already exists.
        /// </summary>
        RPC_S_ENTRY_ALREADY_EXISTS = 1760,

        /// <summary>
        /// The entry is not found.
        /// </summary>
        RPC_S_ENTRY_NOT_FOUND = 1761,

        /// <summary>
        /// The name service is unavailable.
        /// </summary>
        RPC_S_NAME_SERVICE_UNAVAILABLE = 1762,

        /// <summary>
        /// The network address family is invalid.
        /// </summary>
        RPC_S_INVALID_NAF_ID = 1763,

        /// <summary>
        /// The requested operation is not supported.
        /// </summary>
        RPC_S_CANNOT_SUPPORT = 1764,

        /// <summary>
        /// No security context is available to allow impersonation.
        /// </summary>
        RPC_S_NO_CONTEXT_AVAILABLE = 1765,

        /// <summary>
        /// An internal error occurred in a remote procedure call (RPC).
        /// </summary>
        RPC_S_INTERNAL_ERROR = 1766,

        /// <summary>
        /// The RPC server attempted an integer division by zero.
        /// </summary>
        RPC_S_ZERO_DIVIDE = 1767,

        /// <summary>
        /// An addressing error occurred in the RPC server.
        /// </summary>
        RPC_S_ADDRESS_ERROR = 1768,

        /// <summary>
        /// A floating-point operation at the RPC server caused a division by zero.
        /// </summary>
        RPC_S_FP_DIV_ZERO = 1769,

        /// <summary>
        /// A floating-point underflow occurred at the RPC server.
        /// </summary>
        RPC_S_FP_UNDERFLOW = 1770,

        /// <summary>
        /// A floating-point overflow occurred at the RPC server.
        /// </summary>
        RPC_S_FP_OVERFLOW = 1771,

        /// <summary>
        /// The list of RPC servers available for the binding of auto handles has been exhausted.
        /// </summary>
        RPC_X_NO_MORE_ENTRIES = 1772,

        /// <summary>
        /// Unable to open the character translation table file.
        /// </summary>
        RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 1773,

        /// <summary>
        /// The file containing the character translation table has fewer than 512 bytes.
        /// </summary>
        RPC_X_SS_CHAR_TRANS_LONG_FILE = 1774,

        /// <summary>
        /// A null context handle was passed from the client to the host during a remote procedure call.
        /// </summary>
        RPC_X_SS_IN_NULL_CONTEXT = 1775,

        /// <summary>
        /// The context handle changed during a remote procedure call.
        /// </summary>
        RPC_X_SS_CONTEXT_DAMAGED = 1777,

        /// <summary>
        /// The binding handles passed to a remote procedure call do not match.
        /// </summary>
        RPC_X_SS_HANDLES_MISMATCH = 1778,

        /// <summary>
        /// The stub is unable to get the remote procedure call handle.
        /// </summary>
        RPC_X_SS_CANNOT_GET_CALL_HANDLE = 1779,

        /// <summary>
        /// A null reference pointer was passed to the stub.
        /// </summary>
        RPC_X_NULL_REF_POINTER = 1780,

        /// <summary>
        /// The enumeration value is out of range.
        /// </summary>
        RPC_X_ENUM_VALUE_OUT_OF_RANGE = 1781,

        /// <summary>
        /// The byte count is too small.
        /// </summary>
        RPC_X_BYTE_COUNT_TOO_SMALL = 1782,

        /// <summary>
        /// The stub received bad data.
        /// </summary>
        RPC_X_BAD_STUB_DATA = 1783,

        /// <summary>
        /// The supplied user buffer is not valid for the requested operation.
        /// </summary>
        INVALID_USER_BUFFER = 1784,

        /// <summary>
        /// The disk media is not recognized. It may not be formatted.
        /// </summary>
        UNRECOGNIZED_MEDIA = 1785,

        /// <summary>
        /// The workstation does not have a trust secret.
        /// </summary>
        NO_TRUST_LSA_SECRET = 1786,

        /// <summary>
        /// The security database on the server does not have a computer account for this workstation trust relationship.
        /// </summary>
        NO_TRUST_SAM_ACCOUNT = 1787,

        /// <summary>
        /// The trust relationship between the primary domain and the trusted domain failed.
        /// </summary>
        TRUSTED_DOMAIN_FAILURE = 1788,

        /// <summary>
        /// The trust relationship between this workstation and the primary domain failed.
        /// </summary>
        TRUSTED_RELATIONSHIP_FAILURE = 1789,

        /// <summary>
        /// The network logon failed.
        /// </summary>
        TRUST_FAILURE = 1790,

        /// <summary>
        /// A remote procedure call is already in progress for this thread.
        /// </summary>
        RPC_S_CALL_IN_PROGRESS = 1791,

        /// <summary>
        /// An attempt was made to logon, but the network logon service was not started.
        /// </summary>
        NETLOGON_NOT_STARTED = 1792,

        /// <summary>
        /// The user's account has expired.
        /// </summary>
        ACCOUNT_EXPIRED = 1793,

        /// <summary>
        /// The redirector is in use and cannot be unloaded.
        /// </summary>
        REDIRECTOR_HAS_OPEN_HANDLES = 1794,

        /// <summary>
        /// The specified printer driver is already installed.
        /// </summary>
        PRINTER_DRIVER_ALREADY_INSTALLED = 1795,

        /// <summary>
        /// The specified port is unknown.
        /// </summary>
        UNKNOWN_PORT = 1796,

        /// <summary>
        /// The printer driver is unknown.
        /// </summary>
        UNKNOWN_PRINTER_DRIVER = 1797,

        /// <summary>
        /// The print processor is unknown.
        /// </summary>
        UNKNOWN_PRINTPROCESSOR = 1798,

        /// <summary>
        /// The specified separator file is invalid.
        /// </summary>
        INVALID_SEPARATOR_FILE = 1799,

        /// <summary>
        /// The specified priority is invalid.
        /// </summary>
        INVALID_PRIORITY = 1800,

        /// <summary>
        /// The printer name is invalid.
        /// </summary>
        INVALID_PRINTER_NAME = 1801,

        /// <summary>
        /// The printer already exists.
        /// </summary>
        PRINTER_ALREADY_EXISTS = 1802,

        /// <summary>
        /// The printer command is invalid.
        /// </summary>
        INVALID_PRINTER_COMMAND = 1803,

        /// <summary>
        /// The specified datatype is invalid.
        /// </summary>
        INVALID_DATATYPE = 1804,

        /// <summary>
        /// The environment specified is invalid.
        /// </summary>
        INVALID_ENVIRONMENT = 1805,

        /// <summary>
        /// There are no more bindings.
        /// </summary>
        RPC_S_NO_MORE_BINDINGS = 1806,

        /// <summary>
        /// The account used is an interdomain trust account. Use your global user account or local user account to access this server.
        /// </summary>
        NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = 1807,

        /// <summary>
        /// The account used is a computer account. Use your global user account or local user account to access this server.
        /// </summary>
        NOLOGON_WORKSTATION_TRUST_ACCOUNT = 1808,

        /// <summary>
        /// The account used is a server trust account. Use your global user account or local user account to access this server.
        /// </summary>
        NOLOGON_SERVER_TRUST_ACCOUNT = 1809,

        /// <summary>
        /// The name or security ID (SID) of the domain specified is inconsistent with the trust information for that domain.
        /// </summary>
        DOMAIN_TRUST_INCONSISTENT = 1810,

        /// <summary>
        /// The server is in use and cannot be unloaded.
        /// </summary>
        SERVER_HAS_OPEN_HANDLES = 1811,

        /// <summary>
        /// The specified image file did not contain a resource section.
        /// </summary>
        RESOURCE_DATA_NOT_FOUND = 1812,

        /// <summary>
        /// The specified resource type cannot be found in the image file.
        /// </summary>
        RESOURCE_TYPE_NOT_FOUND = 1813,

        /// <summary>
        /// The specified resource name cannot be found in the image file.
        /// </summary>
        RESOURCE_NAME_NOT_FOUND = 1814,

        /// <summary>
        /// The specified resource language ID cannot be found in the image file.
        /// </summary>
        RESOURCE_LANG_NOT_FOUND = 1815,

        /// <summary>
        /// Not enough quota is available to process this command.
        /// </summary>
        NOT_ENOUGH_QUOTA = 1816,

        /// <summary>
        /// No interfaces have been registered.
        /// </summary>
        RPC_S_NO_INTERFACES = 1817,

        /// <summary>
        /// The remote procedure call was cancelled.
        /// </summary>
        RPC_S_CALL_CANCELLED = 1818,

        /// <summary>
        /// The binding handle does not contain all required information.
        /// </summary>
        RPC_S_BINDING_INCOMPLETE = 1819,

        /// <summary>
        /// A communications failure occurred during a remote procedure call.
        /// </summary>
        RPC_S_COMM_FAILURE = 1820,

        /// <summary>
        /// The requested authentication level is not supported.
        /// </summary>
        RPC_S_UNSUPPORTED_AUTHN_LEVEL = 1821,

        /// <summary>
        /// No principal name registered.
        /// </summary>
        RPC_S_NO_PRINC_NAME = 1822,

        /// <summary>
        /// The error specified is not a valid Windows RPC error code.
        /// </summary>
        RPC_S_NOT_RPC_ERROR = 1823,

        /// <summary>
        /// A UUID that is valid only on this computer has been allocated.
        /// </summary>
        RPC_S_UUID_LOCAL_ONLY = 1824,

        /// <summary>
        /// A security package specific error occurred.
        /// </summary>
        RPC_S_SEC_PKG_ERROR = 1825,

        /// <summary>
        /// Thread is not canceled.
        /// </summary>
        RPC_S_NOT_CANCELLED = 1826,

        /// <summary>
        /// Invalid operation on the encoding/decoding handle.
        /// </summary>
        RPC_X_INVALID_ES_ACTION = 1827,

        /// <summary>
        /// Incompatible version of the serializing package.
        /// </summary>
        RPC_X_WRONG_ES_VERSION = 1828,

        /// <summary>
        /// Incompatible version of the RPC stub.
        /// </summary>
        RPC_X_WRONG_STUB_VERSION = 1829,

        /// <summary>
        /// The RPC pipe object is invalid or corrupted.
        /// </summary>
        RPC_X_INVALID_PIPE_OBJECT = 1830,

        /// <summary>
        /// An invalid operation was attempted on an RPC pipe object.
        /// </summary>
        RPC_X_WRONG_PIPE_ORDER = 1831,

        /// <summary>
        /// Unsupported RPC pipe version.
        /// </summary>
        RPC_X_WRONG_PIPE_VERSION = 1832,

        /// <summary>
        /// The group member was not found.
        /// </summary>
        RPC_S_GROUP_MEMBER_NOT_FOUND = 1898,

        /// <summary>
        /// The endpoint mapper database entry could not be created.
        /// </summary>
        EPT_S_CANT_CREATE = 1899,

        /// <summary>
        /// The object universal unique identifier (UUID) is the nil UUID.
        /// </summary>
        RPC_S_INVALID_OBJECT = 1900,

        /// <summary>
        /// The specified time is invalid.
        /// </summary>
        INVALID_TIME = 1901,

        /// <summary>
        /// The specified form name is invalid.
        /// </summary>
        INVALID_FORM_NAME = 1902,

        /// <summary>
        /// The specified form size is invalid.
        /// </summary>
        INVALID_FORM_SIZE = 1903,

        /// <summary>
        /// The specified printer handle is already being waited on
        /// </summary>
        ALREADY_WAITING = 1904,

        /// <summary>
        /// The specified printer has been deleted.
        /// </summary>
        PRINTER_DELETED = 1905,

        /// <summary>
        /// The state of the printer is invalid.
        /// </summary>
        INVALID_PRINTER_STATE = 1906,

        /// <summary>
        /// The user's password must be changed before logging on the first time.
        /// </summary>
        PASSWORD_MUST_CHANGE = 1907,

        /// <summary>
        /// Could not find the domain controller for this domain.
        /// </summary>
        DOMAIN_CONTROLLER_NOT_FOUND = 1908,

        /// <summary>
        /// The referenced account is currently locked out and may not be logged on to.
        /// </summary>
        ACCOUNT_LOCKED_OUT = 1909,

        /// <summary>
        /// The object exporter specified was not found.
        /// </summary>
        OR_INVALID_OXID = 1910,

        /// <summary>
        /// The object specified was not found.
        /// </summary>
        OR_INVALID_OID = 1911,

        /// <summary>
        /// The object resolver set specified was not found.
        /// </summary>
        OR_INVALID_SET = 1912,

        /// <summary>
        /// Some data remains to be sent in the request buffer.
        /// </summary>
        RPC_S_SEND_INCOMPLETE = 1913,

        /// <summary>
        /// Invalid asynchronous remote procedure call handle.
        /// </summary>
        RPC_S_INVALID_ASYNC_HANDLE = 1914,

        /// <summary>
        /// Invalid asynchronous RPC call handle for this operation.
        /// </summary>
        RPC_S_INVALID_ASYNC_CALL = 1915,

        /// <summary>
        /// The RPC pipe object has already been closed.
        /// </summary>
        RPC_X_PIPE_CLOSED = 1916,

        /// <summary>
        /// The RPC call completed before all pipes were processed.
        /// </summary>
        RPC_X_PIPE_DISCIPLINE_ERROR = 1917,

        /// <summary>
        /// No more data is available from the RPC pipe.
        /// </summary>
        RPC_X_PIPE_EMPTY = 1918,

        /// <summary>
        /// No site name is available for this machine.
        /// </summary>
        NO_SITENAME = 1919,

        /// <summary>
        /// The file can not be accessed by the system.
        /// </summary>
        CANT_ACCESS_FILE = 1920,

        /// <summary>
        /// The name of the file cannot be resolved by the system.
        /// </summary>
        CANT_RESOLVE_FILENAME = 1921,

        /// <summary>
        /// The entry is not of the expected type.
        /// </summary>
        RPC_S_ENTRY_TYPE_MISMATCH = 1922,

        /// <summary>
        /// Not all object UUIDs could be exported to the specified entry.
        /// </summary>
        RPC_S_NOT_ALL_OBJS_EXPORTED = 1923,

        /// <summary>
        /// Interface could not be exported to the specified entry.
        /// </summary>
        RPC_S_INTERFACE_NOT_EXPORTED = 1924,

        /// <summary>
        /// The specified profile entry could not be added.
        /// </summary>
        RPC_S_PROFILE_NOT_ADDED = 1925,

        /// <summary>
        /// The specified profile element could not be added.
        /// </summary>
        RPC_S_PRF_ELT_NOT_ADDED = 1926,

        /// <summary>
        /// The specified profile element could not be removed.
        /// </summary>
        RPC_S_PRF_ELT_NOT_REMOVED = 1927,

        /// <summary>
        /// The group element could not be added.
        /// </summary>
        RPC_S_GRP_ELT_NOT_ADDED = 1928,

        /// <summary>
        /// The group element could not be removed.
        /// </summary>
        RPC_S_GRP_ELT_NOT_REMOVED = 1929,

        /// <summary>
        /// The printer driver is not compatible with a policy enabled on your computer that blocks NT 4.0 drivers.
        /// </summary>
        KM_DRIVER_BLOCKED = 1930,

        /// <summary>
        /// The context has expired and can no longer be used.
        /// </summary>
        CONTEXT_EXPIRED = 1931,

        /// <summary>
        /// No information avialable.
        /// </summary>
        PER_USER_TRUST_QUOTA_EXCEEDED = 1932,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ALL_USER_TRUST_QUOTA_EXCEEDED = 1933,

        /// <summary>
        /// No information avialable.
        /// </summary>
        USER_DELETE_TRUST_QUOTA_EXCEEDED = 1934,

        /// <summary>
        /// No information avialable.
        /// </summary>
        AUTHENTICATION_FIREWALL_FAILED = 1935,

        /// <summary>
        /// No information avialable.
        /// </summary>
        REMOTE_PRINT_CONNECTIONS_BLOCKED = 1936,

        /// <summary>
        /// The pixel format is invalid.
        /// </summary>
        INVALID_PIXEL_FORMAT = 2000,

        /// <summary>
        /// The specified driver is invalid.
        /// </summary>
        BAD_DRIVER = 2001,

        /// <summary>
        /// The window style or class attribute is invalid for this operation.
        /// </summary>
        INVALID_WINDOW_STYLE = 2002,

        /// <summary>
        /// The requested metafile operation is not supported.
        /// </summary>
        METAFILE_NOT_SUPPORTED = 2003,

        /// <summary>
        /// The requested transformation operation is not supported.
        /// </summary>
        TRANSFORM_NOT_SUPPORTED = 2004,

        /// <summary>
        /// The requested clipping operation is not supported.
        /// </summary>
        CLIPPING_NOT_SUPPORTED = 2005,

        /// <summary>
        /// The specified color management module is invalid.
        /// </summary>
        INVALID_CMM = 2010,

        /// <summary>
        /// The specified color profile is invalid.
        /// </summary>
        INVALID_PROFILE = 2011,

        /// <summary>
        /// The specified tag was not found.
        /// </summary>
        TAG_NOT_FOUND = 2012,

        /// <summary>
        /// A required tag is not present.
        /// </summary>
        TAG_NOT_PRESENT = 2013,

        /// <summary>
        /// The specified tag is already present.
        /// </summary>
        DUPLICATE_TAG = 2014,

        /// <summary>
        /// The specified color profile is not associated with any device.
        /// </summary>
        PROFILE_NOT_ASSOCIATED_WITH_DEVICE = 2015,

        /// <summary>
        /// The specified color profile was not found.
        /// </summary>
        PROFILE_NOT_FOUND = 2016,

        /// <summary>
        /// The specified color space is invalid.
        /// </summary>
        INVALID_COLORSPACE = 2017,

        /// <summary>
        /// Image Color Management is not enabled.
        /// </summary>
        ICM_NOT_ENABLED = 2018,

        /// <summary>
        /// There was an error while deleting the color transform.
        /// </summary>
        DELETING_ICM_XFORM = 2019,

        /// <summary>
        /// The specified color transform is invalid.
        /// </summary>
        INVALID_TRANSFORM = 2020,

        /// <summary>
        /// The specified transform does not match the bitmap's color space.
        /// </summary>
        COLORSPACE_MISMATCH = 2021,

        /// <summary>
        /// The specified named color index is not present in the profile.
        /// </summary>
        INVALID_COLORINDEX = 2022,

        /// <summary>
        /// The network connection was made successfully, but the user had to be prompted for a password other than the one originally specified.
        /// </summary>
        CONNECTED_OTHER_PASSWORD = 2108,

        /// <summary>
        /// The network connection was made successfully using default credentials.
        /// </summary>
        CONNECTED_OTHER_PASSWORD_DEFAULT = 2109,

        /// <summary>
        /// The specified username is invalid.
        /// </summary>
        BAD_USERNAME = 2202,

        /// <summary>
        /// This network connection does not exist.
        /// </summary>
        NOT_CONNECTED = 2250,

        /// <summary>
        /// This network connection has files open or requests pending.
        /// </summary>
        OPEN_FILES = 2401,

        /// <summary>
        /// Active connections still exist.
        /// </summary>
        ACTIVE_CONNECTIONS = 2402,

        /// <summary>
        /// The device is in use by an active process and cannot be disconnected.
        /// </summary>
        DEVICE_IN_USE = 2404,

        /// <summary>
        /// The specified print monitor is unknown.
        /// </summary>
        UNKNOWN_PRINT_MONITOR = 3000,

        /// <summary>
        /// The specified printer driver is currently in use.
        /// </summary>
        PRINTER_DRIVER_IN_USE = 3001,

        /// <summary>
        /// The spool file was not found.
        /// </summary>
        SPOOL_FILE_NOT_FOUND = 3002,

        /// <summary>
        /// A StartDocPrinter call was not issued.
        /// </summary>
        SPL_NO_STARTDOC = 3003,

        /// <summary>
        /// An AddJob call was not issued.
        /// </summary>
        SPL_NO_ADDJOB = 3004,

        /// <summary>
        /// The specified print processor has already been installed.
        /// </summary>
        PRINT_PROCESSOR_ALREADY_INSTALLED = 3005,

        /// <summary>
        /// The specified print monitor has already been installed.
        /// </summary>
        PRINT_MONITOR_ALREADY_INSTALLED = 3006,

        /// <summary>
        /// The specified print monitor does not have the required functions.
        /// </summary>
        INVALID_PRINT_MONITOR = 3007,

        /// <summary>
        /// The specified print monitor is currently in use.
        /// </summary>
        PRINT_MONITOR_IN_USE = 3008,

        /// <summary>
        /// The requested operation is not allowed when there are jobs queued to the printer.
        /// </summary>
        PRINTER_HAS_JOBS_QUEUED = 3009,

        /// <summary>
        /// The requested operation is successful. Changes will not be effective until the system is rebooted.
        /// </summary>
        SUCCESS_REBOOT_REQUIRED = 3010,

        /// <summary>
        /// The requested operation is successful. Changes will not be effective until the service is restarted.
        /// </summary>
        SUCCESS_RESTART_REQUIRED = 3011,

        /// <summary>
        /// No printers were found.
        /// </summary>
        PRINTER_NOT_FOUND = 3012,

        /// <summary>
        /// The printer driver is known to be unreliable.
        /// </summary>
        PRINTER_DRIVER_WARNED = 3013,

        /// <summary>
        /// The printer driver is known to harm the system.
        /// </summary>
        PRINTER_DRIVER_BLOCKED = 3014,

        /// <summary>
        /// WINS encountered an error while processing the command.
        /// </summary>
        WINS_INTERNAL = 4000,

        /// <summary>
        /// The local WINS can not be deleted.
        /// </summary>
        CAN_NOT_DEL_LOCAL_WINS = 4001,

        /// <summary>
        /// The importation from the file failed.
        /// </summary>
        STATIC_INIT = 4002,

        /// <summary>
        /// The backup failed. Was a full backup done before?
        /// </summary>
        INC_BACKUP = 4003,

        /// <summary>
        /// The backup failed. Check the directory to which you are backing the database.
        /// </summary>
        FULL_BACKUP = 4004,

        /// <summary>
        /// The name does not exist in the WINS database.
        /// </summary>
        REC_NON_EXISTENT = 4005,

        /// <summary>
        /// Replication with a nonconfigured partner is not allowed.
        /// </summary>
        RPL_NOT_ALLOWED = 4006,

        /// <summary>
        /// The DHCP client has obtained an IP address that is already in use on the network. The local interface will be disabled until the DHCP client can obtain a new address.
        /// </summary>
        DHCP_ADDRESS_CONFLICT = 4100,

        /// <summary>
        /// The GUID passed was not recognized as valid by a WMI data provider.
        /// </summary>
        WMI_GUID_NOT_FOUND = 4200,

        /// <summary>
        /// The instance name passed was not recognized as valid by a WMI data provider.
        /// </summary>
        WMI_INSTANCE_NOT_FOUND = 4201,

        /// <summary>
        /// The data item ID passed was not recognized as valid by a WMI data provider.
        /// </summary>
        WMI_ITEMID_NOT_FOUND = 4202,

        /// <summary>
        /// The WMI request could not be completed and should be retried.
        /// </summary>
        WMI_TRY_AGAIN = 4203,

        /// <summary>
        /// The WMI data provider could not be located.
        /// </summary>
        WMI_DP_NOT_FOUND = 4204,

        /// <summary>
        /// The WMI data provider references an instance set that has not been registered.
        /// </summary>
        WMI_UNRESOLVED_INSTANCE_REF = 4205,

        /// <summary>
        /// The WMI data block or event notification has already been enabled.
        /// </summary>
        WMI_ALREADY_ENABLED = 4206,

        /// <summary>
        /// The WMI data block is no longer available.
        /// </summary>
        WMI_GUID_DISCONNECTED = 4207,

        /// <summary>
        /// The WMI data service is not available.
        /// </summary>
        WMI_SERVER_UNAVAILABLE = 4208,

        /// <summary>
        /// The WMI data provider failed to carry out the request.
        /// </summary>
        WMI_DP_FAILED = 4209,

        /// <summary>
        /// The WMI MOF information is not valid.
        /// </summary>
        WMI_INVALID_MOF = 4210,

        /// <summary>
        /// The WMI registration information is not valid.
        /// </summary>
        WMI_INVALID_REGINFO = 4211,

        /// <summary>
        /// The WMI data block or event notification has already been disabled.
        /// </summary>
        WMI_ALREADY_DISABLED = 4212,

        /// <summary>
        /// The WMI data item or data block is read only.
        /// </summary>
        WMI_READ_ONLY = 4213,

        /// <summary>
        /// The WMI data item or data block could not be changed.
        /// </summary>
        WMI_SET_FAILURE = 4214,

        /// <summary>
        /// The media identifier does not represent a valid medium.
        /// </summary>
        INVALID_MEDIA = 4300,

        /// <summary>
        /// The library identifier does not represent a valid library.
        /// </summary>
        INVALID_LIBRARY = 4301,

        /// <summary>
        /// The media pool identifier does not represent a valid media pool.
        /// </summary>
        INVALID_MEDIA_POOL = 4302,

        /// <summary>
        /// The drive and medium are not compatible or exist in different libraries.
        /// </summary>
        DRIVE_MEDIA_MISMATCH = 4303,

        /// <summary>
        /// The medium currently exists in an offline library and must be online to perform this operation.
        /// </summary>
        MEDIA_OFFLINE = 4304,

        /// <summary>
        /// The operation cannot be performed on an offline library.
        /// </summary>
        LIBRARY_OFFLINE = 4305,

        /// <summary>
        /// The library, drive, or media pool is empty.
        /// </summary>
        EMPTY = 4306,

        /// <summary>
        /// The library, drive, or media pool must be empty to perform this operation.
        /// </summary>
        NOT_EMPTY = 4307,

        /// <summary>
        /// No media is currently available in this media pool or library.
        /// </summary>
        MEDIA_UNAVAILABLE = 4308,

        /// <summary>
        /// A resource required for this operation is disabled.
        /// </summary>
        RESOURCE_DISABLED = 4309,

        /// <summary>
        /// The media identifier does not represent a valid cleaner.
        /// </summary>
        INVALID_CLEANER = 4310,

        /// <summary>
        /// The drive cannot be cleaned or does not support cleaning.
        /// </summary>
        UNABLE_TO_CLEAN = 4311,

        /// <summary>
        /// The object identifier does not represent a valid object.
        /// </summary>
        OBJECT_NOT_FOUND = 4312,

        /// <summary>
        /// Unable to read from or write to the database.
        /// </summary>
        DATABASE_FAILURE = 4313,

        /// <summary>
        /// The database is full.
        /// </summary>
        DATABASE_FULL = 4314,

        /// <summary>
        /// The medium is not compatible with the device or media pool.
        /// </summary>
        MEDIA_INCOMPATIBLE = 4315,

        /// <summary>
        /// The resource required for this operation does not exist.
        /// </summary>
        RESOURCE_NOT_PRESENT = 4316,

        /// <summary>
        /// The operation identifier is not valid.
        /// </summary>
        INVALID_OPERATION = 4317,

        /// <summary>
        /// The media is not mounted or ready for use.
        /// </summary>
        MEDIA_NOT_AVAILABLE = 4318,

        /// <summary>
        /// The device is not ready for use.
        /// </summary>
        DEVICE_NOT_AVAILABLE = 4319,

        /// <summary>
        /// The operator or administrator has refused the request.
        /// </summary>
        REQUEST_REFUSED = 4320,

        /// <summary>
        /// The drive identifier does not represent a valid drive.
        /// </summary>
        INVALID_DRIVE_OBJECT = 4321,

        /// <summary>
        /// Library is full.  No slot is available for use.
        /// </summary>
        LIBRARY_FULL = 4322,

        /// <summary>
        /// The transport cannot access the medium.
        /// </summary>
        MEDIUM_NOT_ACCESSIBLE = 4323,

        /// <summary>
        /// Unable to load the medium into the drive.
        /// </summary>
        UNABLE_TO_LOAD_MEDIUM = 4324,

        /// <summary>
        /// Unable to retrieve the drive status.
        /// </summary>
        UNABLE_TO_INVENTORY_DRIVE = 4325,

        /// <summary>
        /// Unable to retrieve the slot status.
        /// </summary>
        UNABLE_TO_INVENTORY_SLOT = 4326,

        /// <summary>
        /// Unable to retrieve status about the transport.
        /// </summary>
        UNABLE_TO_INVENTORY_TRANSPORT = 4327,

        /// <summary>
        /// Cannot use the transport because it is already in use.
        /// </summary>
        TRANSPORT_FULL = 4328,

        /// <summary>
        /// Unable to open or close the inject/eject port.
        /// </summary>
        CONTROLLING_IEPORT = 4329,

        /// <summary>
        /// Unable to eject the medium because it is in a drive.
        /// </summary>
        UNABLE_TO_EJECT_MOUNTED_MEDIA = 4330,

        /// <summary>
        /// A cleaner slot is already reserved.
        /// </summary>
        CLEANER_SLOT_SET = 4331,

        /// <summary>
        /// A cleaner slot is not reserved.
        /// </summary>
        CLEANER_SLOT_NOT_SET = 4332,

        /// <summary>
        /// The cleaner cartridge has performed the maximum number of drive cleanings.
        /// </summary>
        CLEANER_CARTRIDGE_SPENT = 4333,

        /// <summary>
        /// Unexpected on-medium identifier.
        /// </summary>
        UNEXPECTED_OMID = 4334,

        /// <summary>
        /// The last remaining item in this group or resource cannot be deleted.
        /// </summary>
        CANT_DELETE_LAST_ITEM = 4335,

        /// <summary>
        /// The message provided exceeds the maximum size allowed for this parameter.
        /// </summary>
        MESSAGE_EXCEEDS_MAX_SIZE = 4336,

        /// <summary>
        /// The volume contains system or paging files.
        /// </summary>
        VOLUME_CONTAINS_SYS_FILES = 4337,

        /// <summary>
        /// The media type cannot be removed from this library since at least one drive in the library reports it can support this media type.
        /// </summary>
        INDIGENOUS_TYPE = 4338,

        /// <summary>
        /// This offline media cannot be mounted on this system since no enabled drives are present which can be used.
        /// </summary>
        NO_SUPPORTING_DRIVES = 4339,

        /// <summary>
        /// A cleaner cartridge is present in the tape library.
        /// </summary>
        CLEANER_CARTRIDGE_INSTALLED = 4340,

        /// <summary>
        /// The remote storage service was not able to recall the file.
        /// </summary>
        FILE_OFFLINE = 4350,

        /// <summary>
        /// The remote storage service is not operational at this time.
        /// </summary>
        REMOTE_STORAGE_NOT_ACTIVE = 4351,

        /// <summary>
        /// The remote storage service encountered a media error.
        /// </summary>
        REMOTE_STORAGE_MEDIA_ERROR = 4352,

        /// <summary>
        /// The file or directory is not a reparse point.
        /// </summary>
        NOT_A_REPARSE_POINT = 4390,

        /// <summary>
        /// The reparse point attribute cannot be set because it conflicts with an existing attribute.
        /// </summary>
        REPARSE_ATTRIBUTE_CONFLICT = 4391,

        /// <summary>
        /// The data present in the reparse point buffer is invalid.
        /// </summary>
        INVALID_REPARSE_DATA = 4392,

        /// <summary>
        /// The tag present in the reparse point buffer is invalid.
        /// </summary>
        REPARSE_TAG_INVALID = 4393,

        /// <summary>
        /// There is a mismatch between the tag specified in the request and the tag present in the reparse point.
        /// </summary>
        REPARSE_TAG_MISMATCH = 4394,

        /// <summary>
        /// Single Instance Storage is not available on this volume.
        /// </summary>
        VOLUME_NOT_SIS_ENABLED = 4500,

        /// <summary>
        /// The cluster resource cannot be moved to another group because other resources are dependent on it.
        /// </summary>
        DEPENDENT_RESOURCE_EXISTS = 5001,

        /// <summary>
        /// The cluster resource dependency cannot be found.
        /// </summary>
        DEPENDENCY_NOT_FOUND = 5002,

        /// <summary>
        /// The cluster resource cannot be made dependent on the specified resource because it is already dependent.
        /// </summary>
        DEPENDENCY_ALREADY_EXISTS = 5003,

        /// <summary>
        /// The cluster resource is not online.
        /// </summary>
        RESOURCE_NOT_ONLINE = 5004,

        /// <summary>
        /// A cluster node is not available for this operation.
        /// </summary>
        HOST_NODE_NOT_AVAILABLE = 5005,

        /// <summary>
        /// The cluster resource is not available.
        /// </summary>
        RESOURCE_NOT_AVAILABLE = 5006,

        /// <summary>
        /// The cluster resource could not be found.
        /// </summary>
        RESOURCE_NOT_FOUND = 5007,

        /// <summary>
        /// The cluster is being shut down.
        /// </summary>
        SHUTDOWN_CLUSTER = 5008,

        /// <summary>
        /// A cluster node cannot be evicted from the cluster unless the node is down or it is the last node.
        /// </summary>
        CANT_EVICT_ACTIVE_NODE = 5009,

        /// <summary>
        /// The object already exists.
        /// </summary>
        OBJECT_ALREADY_EXISTS = 5010,

        /// <summary>
        /// The object is already in the list.
        /// </summary>
        OBJECT_IN_LIST = 5011,

        /// <summary>
        /// The cluster group is not available for any new requests.
        /// </summary>
        GROUP_NOT_AVAILABLE = 5012,

        /// <summary>
        /// The cluster group could not be found.
        /// </summary>
        GROUP_NOT_FOUND = 5013,

        /// <summary>
        /// The operation could not be completed because the cluster group is not online.
        /// </summary>
        GROUP_NOT_ONLINE = 5014,

        /// <summary>
        /// The cluster node is not the owner of the resource.
        /// </summary>
        HOST_NODE_NOT_RESOURCE_OWNER = 5015,

        /// <summary>
        /// The cluster node is not the owner of the group.
        /// </summary>
        HOST_NODE_NOT_GROUP_OWNER = 5016,

        /// <summary>
        /// The cluster resource could not be created in the specified resource monitor.
        /// </summary>
        RESMON_CREATE_FAILED = 5017,

        /// <summary>
        /// The cluster resource could not be brought online by the resource monitor.
        /// </summary>
        RESMON_ONLINE_FAILED = 5018,

        /// <summary>
        /// The operation could not be completed because the cluster resource is online.
        /// </summary>
        RESOURCE_ONLINE = 5019,

        /// <summary>
        /// The cluster resource could not be deleted or brought offline because it is the quorum resource.
        /// </summary>
        QUORUM_RESOURCE = 5020,

        /// <summary>
        /// The cluster could not make the specified resource a quorum resource because it is not capable of being a quorum resource.
        /// </summary>
        NOT_QUORUM_CAPABLE = 5021,

        /// <summary>
        /// The cluster software is shutting down.
        /// </summary>
        CLUSTER_SHUTTING_DOWN = 5022,

        /// <summary>
        /// The group or resource is not in the correct state to perform the requested operation.
        /// </summary>
        INVALID_STATE = 5023,

        /// <summary>
        /// The properties were stored but not all changes will take effect until the next time the resource is brought online.
        /// </summary>
        RESOURCE_PROPERTIES_STORED = 5024,

        /// <summary>
        /// The cluster could not make the specified resource a quorum resource because it does not belong to a shared storage class.
        /// </summary>
        NOT_QUORUM_CLASS = 5025,

        /// <summary>
        /// The cluster resource could not be deleted since it is a core resource.
        /// </summary>
        CORE_RESOURCE = 5026,

        /// <summary>
        /// The quorum resource failed to come online.
        /// </summary>
        QUORUM_RESOURCE_ONLINE_FAILED = 5027,

        /// <summary>
        /// The quorum log could not be created or mounted successfully.
        /// </summary>
        QUORUMLOG_OPEN_FAILED = 5028,

        /// <summary>
        /// The cluster log is corrupt.
        /// </summary>
        CLUSTERLOG_CORRUPT = 5029,

        /// <summary>
        /// The record could not be written to the cluster log since it exceeds the maximum size.
        /// </summary>
        CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE = 5030,

        /// <summary>
        /// The cluster log exceeds its maximum size.
        /// </summary>
        CLUSTERLOG_EXCEEDS_MAXSIZE = 5031,

        /// <summary>
        /// No checkpoint record was found in the cluster log.
        /// </summary>
        CLUSTERLOG_CHKPOINT_NOT_FOUND = 5032,

        /// <summary>
        /// The minimum required disk space needed for logging is not available.
        /// </summary>
        CLUSTERLOG_NOT_ENOUGH_SPACE = 5033,

        /// <summary>
        /// The cluster node failed to take control of the quorum resource because the resource is owned by another active node.
        /// </summary>
        QUORUM_OWNER_ALIVE = 5034,

        /// <summary>
        /// A cluster network is not available for this operation.
        /// </summary>
        NETWORK_NOT_AVAILABLE = 5035,

        /// <summary>
        /// A cluster node is not available for this operation.
        /// </summary>
        NODE_NOT_AVAILABLE = 5036,

        /// <summary>
        /// All cluster nodes must be running to perform this operation.
        /// </summary>
        ALL_NODES_NOT_AVAILABLE = 5037,

        /// <summary>
        /// A cluster resource failed.
        /// </summary>
        RESOURCE_FAILED = 5038,

        /// <summary>
        /// The cluster node is not valid.
        /// </summary>
        CLUSTER_INVALID_NODE = 5039,

        /// <summary>
        /// The cluster node already exists.
        /// </summary>
        CLUSTER_NODE_EXISTS = 5040,

        /// <summary>
        /// A node is in the process of joining the cluster.
        /// </summary>
        CLUSTER_JOIN_IN_PROGRESS = 5041,

        /// <summary>
        /// The cluster node was not found.
        /// </summary>
        CLUSTER_NODE_NOT_FOUND = 5042,

        /// <summary>
        /// The cluster local node information was not found.
        /// </summary>
        CLUSTER_LOCAL_NODE_NOT_FOUND = 5043,

        /// <summary>
        /// The cluster network already exists.
        /// </summary>
        CLUSTER_NETWORK_EXISTS = 5044,

        /// <summary>
        /// The cluster network was not found.
        /// </summary>
        CLUSTER_NETWORK_NOT_FOUND = 5045,

        /// <summary>
        /// The cluster network interface already exists.
        /// </summary>
        CLUSTER_NETINTERFACE_EXISTS = 5046,

        /// <summary>
        /// The cluster network interface was not found.
        /// </summary>
        CLUSTER_NETINTERFACE_NOT_FOUND = 5047,

        /// <summary>
        /// The cluster request is not valid for this object.
        /// </summary>
        CLUSTER_INVALID_REQUEST = 5048,

        /// <summary>
        /// The cluster network provider is not valid.
        /// </summary>
        CLUSTER_INVALID_NETWORK_PROVIDER = 5049,

        /// <summary>
        /// The cluster node is down.
        /// </summary>
        CLUSTER_NODE_DOWN = 5050,

        /// <summary>
        /// The cluster node is not reachable.
        /// </summary>
        CLUSTER_NODE_UNREACHABLE = 5051,

        /// <summary>
        /// The cluster node is not a member of the cluster.
        /// </summary>
        CLUSTER_NODE_NOT_MEMBER = 5052,

        /// <summary>
        /// A cluster join operation is not in progress.
        /// </summary>
        CLUSTER_JOIN_NOT_IN_PROGRESS = 5053,

        /// <summary>
        /// The cluster network is not valid.
        /// </summary>
        CLUSTER_INVALID_NETWORK = 5054,

        /// <summary>
        /// The cluster node is up.
        /// </summary>
        CLUSTER_NODE_UP = 5056,

        /// <summary>
        /// The cluster IP address is already in use.
        /// </summary>
        CLUSTER_IPADDR_IN_USE = 5057,

        /// <summary>
        /// The cluster node is not paused.
        /// </summary>
        CLUSTER_NODE_NOT_PAUSED = 5058,

        /// <summary>
        /// No cluster security context is available.
        /// </summary>
        CLUSTER_NO_SECURITY_CONTEXT = 5059,

        /// <summary>
        /// The cluster network is not configured for internal cluster communication.
        /// </summary>
        CLUSTER_NETWORK_NOT_INTERNAL = 5060,

        /// <summary>
        /// The cluster node is already up.
        /// </summary>
        CLUSTER_NODE_ALREADY_UP = 5061,

        /// <summary>
        /// The cluster node is already down.
        /// </summary>
        CLUSTER_NODE_ALREADY_DOWN = 5062,

        /// <summary>
        /// The cluster network is already online.
        /// </summary>
        CLUSTER_NETWORK_ALREADY_ONLINE = 5063,

        /// <summary>
        /// The cluster network is already offline.
        /// </summary>
        CLUSTER_NETWORK_ALREADY_OFFLINE = 5064,

        /// <summary>
        /// The cluster node is already a member of the cluster.
        /// </summary>
        CLUSTER_NODE_ALREADY_MEMBER = 5065,

        /// <summary>
        /// The cluster network is the only one configured for internal cluster communication between two or more active cluster nodes. The internal communication capability cannot be removed from the network.
        /// </summary>
        CLUSTER_LAST_INTERNAL_NETWORK = 5066,

        /// <summary>
        /// One or more cluster resources depend on the network to provide service to clients. The client access capability cannot be removed from the network.
        /// </summary>
        CLUSTER_NETWORK_HAS_DEPENDENTS = 5067,

        /// <summary>
        /// This operation cannot be performed on the cluster resource as it the quorum resource. You may not bring the quorum resource offline or modify its possible owners list.
        /// </summary>
        INVALID_OPERATION_ON_QUORUM = 5068,

        /// <summary>
        /// The cluster quorum resource is not allowed to have any dependencies.
        /// </summary>
        DEPENDENCY_NOT_ALLOWED = 5069,

        /// <summary>
        /// The cluster node is paused.
        /// </summary>
        CLUSTER_NODE_PAUSED = 5070,

        /// <summary>
        /// The cluster resource cannot be brought online. The owner node cannot run this resource.
        /// </summary>
        NODE_CANT_HOST_RESOURCE = 5071,

        /// <summary>
        /// The cluster node is not ready to perform the requested operation.
        /// </summary>
        CLUSTER_NODE_NOT_READY = 5072,

        /// <summary>
        /// The cluster node is shutting down.
        /// </summary>
        CLUSTER_NODE_SHUTTING_DOWN = 5073,

        /// <summary>
        /// The cluster join operation was aborted.
        /// </summary>
        CLUSTER_JOIN_ABORTED = 5074,

        /// <summary>
        /// The cluster join operation failed due to incompatible software versions between the joining node and its sponsor.
        /// </summary>
        CLUSTER_INCOMPATIBLE_VERSIONS = 5075,

        /// <summary>
        /// This resource cannot be created because the cluster has reached the limit on the number of resources it can monitor.
        /// </summary>
        CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED = 5076,

        /// <summary>
        /// The system configuration changed during the cluster join or form operation. The join or form operation was aborted.
        /// </summary>
        CLUSTER_SYSTEM_CONFIG_CHANGED = 5077,

        /// <summary>
        /// The specified resource type was not found.
        /// </summary>
        CLUSTER_RESOURCE_TYPE_NOT_FOUND = 5078,

        /// <summary>
        /// The specified node does not support a resource of this type.  This may be due to version inconsistencies or due to the absence of the resource DLL on this node.
        /// </summary>
        CLUSTER_RESTYPE_NOT_SUPPORTED = 5079,

        /// <summary>
        /// The specified resource name is not supported by this resource DLL. This may be due to a bad (or changed) name supplied to the resource DLL.
        /// </summary>
        CLUSTER_RESNAME_NOT_FOUND = 5080,

        /// <summary>
        /// No authentication package could be registered with the RPC server.
        /// </summary>
        CLUSTER_NO_RPC_PACKAGES_REGISTERED = 5081,

        /// <summary>
        /// You cannot bring the group online because the owner of the group is not in the preferred list for the group. To change the owner node for the group, move the group.
        /// </summary>
        CLUSTER_OWNER_NOT_IN_PREFLIST = 5082,

        /// <summary>
        /// The join operation failed because the cluster database sequence number has changed or is incompatible with the locker node. This may happen during a join operation if the cluster database was changing during the join.
        /// </summary>
        CLUSTER_DATABASE_SEQMISMATCH = 5083,

        /// <summary>
        /// The resource monitor will not allow the fail operation to be performed while the resource is in its current state. This may happen if the resource is in a pending state.
        /// </summary>
        RESMON_INVALID_STATE = 5084,

        /// <summary>
        /// A non locker code got a request to reserve the lock for making global updates.
        /// </summary>
        CLUSTER_GUM_NOT_LOCKER = 5085,

        /// <summary>
        /// The quorum disk could not be located by the cluster service.
        /// </summary>
        QUORUM_DISK_NOT_FOUND = 5086,

        /// <summary>
        /// The backed up cluster database is possibly corrupt.
        /// </summary>
        DATABASE_BACKUP_CORRUPT = 5087,

        /// <summary>
        /// A DFS root already exists in this cluster node.
        /// </summary>
        CLUSTER_NODE_ALREADY_HAS_DFS_ROOT = 5088,

        /// <summary>
        /// An attempt to modify a resource property failed because it conflicts with another existing property.
        /// </summary>
        RESOURCE_PROPERTY_UNCHANGEABLE = 5089,

        /// <summary>
        /// An operation was attempted that is incompatible with the current membership state of the node.
        /// </summary>
        CLUSTER_MEMBERSHIP_INVALID_STATE = 5890,

        /// <summary>
        /// The quorum resource does not contain the quorum log.
        /// </summary>
        CLUSTER_QUORUMLOG_NOT_FOUND = 5891,

        /// <summary>
        /// The membership engine requested shutdown of the cluster service on this node.
        /// </summary>
        CLUSTER_MEMBERSHIP_HALT = 5892,

        /// <summary>
        /// The join operation failed because the cluster instance ID of the joining node does not match the cluster instance ID of the sponsor node.
        /// </summary>
        CLUSTER_INSTANCE_ID_MISMATCH = 5893,

        /// <summary>
        /// A matching network for the specified IP address could not be found. Please also specify a subnet mask and a cluster network.
        /// </summary>
        CLUSTER_NETWORK_NOT_FOUND_FOR_IP = 5894,

        /// <summary>
        /// The actual data type of the property did not match the expected data type of the property.
        /// </summary>
        CLUSTER_PROPERTY_DATA_TYPE_MISMATCH = 5895,

        /// <summary>
        /// The cluster node was evicted from the cluster successfully, but the node was not cleaned up.  Extended status information explaining why the node was not cleaned up is available.
        /// </summary>
        CLUSTER_EVICT_WITHOUT_CLEANUP = 5896,

        /// <summary>
        /// Two or more parameter values specified for a resource's properties are in conflict.
        /// </summary>
        CLUSTER_PARAMETER_MISMATCH = 5897,

        /// <summary>
        /// This computer cannot be made a member of a cluster.
        /// </summary>
        NODE_CANNOT_BE_CLUSTERED = 5898,

        /// <summary>
        /// This computer cannot be made a member of a cluster because it does not have the correct version of Windows installed.
        /// </summary>
        CLUSTER_WRONG_OS_VERSION = 5899,

        /// <summary>
        /// A cluster cannot be created with the specified cluster name because that cluster name is already in use. Specify a different name for the cluster.
        /// </summary>
        CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME = 5900,

        /// <summary>
        /// No information avialable.
        /// </summary>
        CLUSCFG_ALREADY_COMMITTED = 5901,

        /// <summary>
        /// No information avialable.
        /// </summary>
        CLUSCFG_ROLLBACK_FAILED = 5902,

        /// <summary>
        /// No information avialable.
        /// </summary>
        CLUSCFG_SYSTEM_DISK_DRIVE_LETTER_CONFLICT = 5903,

        /// <summary>
        /// No information avialable.
        /// </summary>
        CLUSTER_OLD_VERSION = 5904,

        /// <summary>
        /// No information avialable.
        /// </summary>
        CLUSTER_MISMATCHED_COMPUTER_ACCT_NAME = 5905,

        /// <summary>
        /// The specified file could not be encrypted.
        /// </summary>
        ENCRYPTION_FAILED = 6000,

        /// <summary>
        /// The specified file could not be decrypted.
        /// </summary>
        DECRYPTION_FAILED = 6001,

        /// <summary>
        /// The specified file is encrypted and the user does not have the ability to decrypt it.
        /// </summary>
        FILE_ENCRYPTED = 6002,

        /// <summary>
        /// There is no valid encryption recovery policy configured for this system.
        /// </summary>
        NO_RECOVERY_POLICY = 6003,

        /// <summary>
        /// The required encryption driver is not loaded for this system.
        /// </summary>
        NO_EFS = 6004,

        /// <summary>
        /// The file was encrypted with a different encryption driver than is currently loaded.
        /// </summary>
        WRONG_EFS = 6005,

        /// <summary>
        /// There are no EFS keys defined for the user.
        /// </summary>
        NO_USER_KEYS = 6006,

        /// <summary>
        /// The specified file is not encrypted.
        /// </summary>
        FILE_NOT_ENCRYPTED = 6007,

        /// <summary>
        /// The specified file is not in the defined EFS export format.
        /// </summary>
        NOT_EXPORT_FORMAT = 6008,

        /// <summary>
        /// The specified file is read only.
        /// </summary>
        FILE_READ_ONLY = 6009,

        /// <summary>
        /// The directory has been disabled for encryption.
        /// </summary>
        DIR_EFS_DISALLOWED = 6010,

        /// <summary>
        /// The server is not trusted for remote encryption operation.
        /// </summary>
        EFS_SERVER_NOT_TRUSTED = 6011,

        /// <summary>
        /// Recovery policy configured for this system contains invalid recovery certificate.
        /// </summary>
        BAD_RECOVERY_POLICY = 6012,

        /// <summary>
        /// The encryption algorithm used on the source file needs a bigger key buffer than the one on the destination file.
        /// </summary>
        EFS_ALG_BLOB_TOO_BIG = 6013,

        /// <summary>
        /// The disk partition does not support file encryption.
        /// </summary>
        VOLUME_NOT_SUPPORT_EFS = 6014,

        /// <summary>
        /// This machine is disabled for file encryption.
        /// </summary>
        EFS_DISABLED = 6015,

        /// <summary>
        /// A newer system is required to decrypt this encrypted file.
        /// </summary>
        EFS_VERSION_NOT_SUPPORT = 6016,

        /// <summary>
        /// The list of servers for this workgroup is not currently available
        /// </summary>
        NO_BROWSER_SERVERS_FOUND = 6118,

        /// <summary>
        /// The Task Scheduler service must be configured to run in the System account to function properly.  Individual tasks may be configured to run in other accounts.
        /// </summary>
        SCHED_E_SERVICE_NOT_LOCALSYSTEM = 6200,

        /// <summary>
        /// The specified session name is invalid.
        /// </summary>
        CTX_WINSTATION_NAME_INVALID = 7001,

        /// <summary>
        /// The specified protocol driver is invalid.
        /// </summary>
        CTX_INVALID_PD = 7002,

        /// <summary>
        /// The specified protocol driver was not found in the system path.
        /// </summary>
        CTX_PD_NOT_FOUND = 7003,

        /// <summary>
        /// The specified terminal connection driver was not found in the system path.
        /// </summary>
        CTX_WD_NOT_FOUND = 7004,

        /// <summary>
        /// A registry key for event logging could not be created for this session.
        /// </summary>
        CTX_CANNOT_MAKE_EVENTLOG_ENTRY = 7005,

        /// <summary>
        /// A service with the same name already exists on the system.
        /// </summary>
        CTX_SERVICE_NAME_COLLISION = 7006,

        /// <summary>
        /// A close operation is pending on the session.
        /// </summary>
        CTX_CLOSE_PENDING = 7007,

        /// <summary>
        /// There are no free output buffers available.
        /// </summary>
        CTX_NO_OUTBUF = 7008,

        /// <summary>
        /// The MODEM.INF file was not found.
        /// </summary>
        CTX_MODEM_INF_NOT_FOUND = 7009,

        /// <summary>
        /// The modem name was not found in MODEM.INF.
        /// </summary>
        CTX_INVALID_MODEMNAME = 7010,

        /// <summary>
        /// The modem did not accept the command sent to it. Verify that the configured modem name matches the attached modem.
        /// </summary>
        CTX_MODEM_RESPONSE_ERROR = 7011,

        /// <summary>
        /// The modem did not respond to the command sent to it. Verify that the modem is properly cabled and powered on.
        /// </summary>
        CTX_MODEM_RESPONSE_TIMEOUT = 7012,

        /// <summary>
        /// Carrier detect has failed or carrier has been dropped due to disconnect.
        /// </summary>
        CTX_MODEM_RESPONSE_NO_CARRIER = 7013,

        /// <summary>
        /// Dial tone not detected within the required time. Verify that the phone cable is properly attached and functional.
        /// </summary>
        CTX_MODEM_RESPONSE_NO_DIALTONE = 7014,

        /// <summary>
        /// Busy signal detected at remote site on callback.
        /// </summary>
        CTX_MODEM_RESPONSE_BUSY = 7015,

        /// <summary>
        /// Voice detected at remote site on callback.
        /// </summary>
        CTX_MODEM_RESPONSE_VOICE = 7016,

        /// <summary>
        /// Transport driver error
        /// </summary>
        CTX_TD_ERROR = 7017,

        /// <summary>
        /// The specified session cannot be found.
        /// </summary>
        CTX_WINSTATION_NOT_FOUND = 7022,

        /// <summary>
        /// The specified session name is already in use.
        /// </summary>
        CTX_WINSTATION_ALREADY_EXISTS = 7023,

        /// <summary>
        /// The requested operation cannot be completed because the terminal connection is currently busy processing a connect, disconnect, reset, or delete operation.
        /// </summary>
        CTX_WINSTATION_BUSY = 7024,

        /// <summary>
        /// An attempt has been made to connect to a session whose video mode is not supported by the current client.
        /// </summary>
        CTX_BAD_VIDEO_MODE = 7025,

        /// <summary>
        /// The application attempted to enable DOS graphics mode.
        /// DOS graphics mode is not supported.
        /// </summary>
        CTX_GRAPHICS_INVALID = 7035,

        /// <summary>
        /// Your interactive logon privilege has been disabled.
        /// Please contact your administrator.
        /// </summary>
        CTX_LOGON_DISABLED = 7037,

        /// <summary>
        /// The requested operation can be performed only on the system console.
        /// This is most often the result of a driver or system DLL requiring direct console access.
        /// </summary>
        CTX_NOT_CONSOLE = 7038,

        /// <summary>
        /// The client failed to respond to the server connect message.
        /// </summary>
        CTX_CLIENT_QUERY_TIMEOUT = 7040,

        /// <summary>
        /// Disconnecting the console session is not supported.
        /// </summary>
        CTX_CONSOLE_DISCONNECT = 7041,

        /// <summary>
        /// Reconnecting a disconnected session to the console is not supported.
        /// </summary>
        CTX_CONSOLE_CONNECT = 7042,

        /// <summary>
        /// The request to control another session remotely was denied.
        /// </summary>
        CTX_SHADOW_DENIED = 7044,

        /// <summary>
        /// The requested session access is denied.
        /// </summary>
        CTX_WINSTATION_ACCESS_DENIED = 7045,

        /// <summary>
        /// The specified terminal connection driver is invalid.
        /// </summary>
        CTX_INVALID_WD = 7049,

        /// <summary>
        /// The requested session cannot be controlled remotely.
        /// This may be because the session is disconnected or does not currently have a user logged on.
        /// </summary>
        CTX_SHADOW_INVALID = 7050,

        /// <summary>
        /// The requested session is not configured to allow remote control.
        /// </summary>
        CTX_SHADOW_DISABLED = 7051,

        /// <summary>
        /// Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number is currently being used by another user.
        /// Please call your system administrator to obtain a unique license number.
        /// </summary>
        CTX_CLIENT_LICENSE_IN_USE = 7052,

        /// <summary>
        /// Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number has not been entered for this copy of the Terminal Server client.
        /// Please contact your system administrator.
        /// </summary>
        CTX_CLIENT_LICENSE_NOT_SET = 7053,

        /// <summary>
        /// The system has reached its licensed logon limit.
        /// Please try again later.
        /// </summary>
        CTX_LICENSE_NOT_AVAILABLE = 7054,

        /// <summary>
        /// The client you are using is not licensed to use this system.  Your logon request is denied.
        /// </summary>
        CTX_LICENSE_CLIENT_INVALID = 7055,

        /// <summary>
        /// The system license has expired.  Your logon request is denied.
        /// </summary>
        CTX_LICENSE_EXPIRED = 7056,

        /// <summary>
        /// Remote control could not be terminated because the specified session is not currently being remotely controlled.
        /// </summary>
        CTX_SHADOW_NOT_RUNNING = 7057,

        /// <summary>
        /// The remote control of the console was terminated because the display mode was changed. Changing the display mode in a remote control session is not supported.
        /// </summary>
        CTX_SHADOW_ENDED_BY_MODE_CHANGE = 7058,

        /// <summary>
        /// No information avialable.
        /// </summary>
        ACTIVATION_COUNT_EXCEEDED = 7059,

        /// <summary>
        /// The file replication service API was called incorrectly.
        /// </summary>
        FRS_ERR_INVALID_API_SEQUENCE = 8001,

        /// <summary>
        /// The file replication service cannot be started.
        /// </summary>
        FRS_ERR_STARTING_SERVICE = 8002,

        /// <summary>
        /// The file replication service cannot be stopped.
        /// </summary>
        FRS_ERR_STOPPING_SERVICE = 8003,

        /// <summary>
        /// The file replication service API terminated the request.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_INTERNAL_API = 8004,

        /// <summary>
        /// The file replication service terminated the request.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_INTERNAL = 8005,

        /// <summary>
        /// The file replication service cannot be contacted.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_SERVICE_COMM = 8006,

        /// <summary>
        /// The file replication service cannot satisfy the request because the user has insufficient privileges.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_INSUFFICIENT_PRIV = 8007,

        /// <summary>
        /// The file replication service cannot satisfy the request because authenticated RPC is not available.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_AUTHENTICATION = 8008,

        /// <summary>
        /// The file replication service cannot satisfy the request because the user has insufficient privileges on the domain controller.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_PARENT_INSUFFICIENT_PRIV = 8009,

        /// <summary>
        /// The file replication service cannot satisfy the request because authenticated RPC is not available on the domain controller.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_PARENT_AUTHENTICATION = 8010,

        /// <summary>
        /// The file replication service cannot communicate with the file replication service on the domain controller.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_CHILD_TO_PARENT_COMM = 8011,

        /// <summary>
        /// The file replication service on the domain controller cannot communicate with the file replication service on this computer.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_PARENT_TO_CHILD_COMM = 8012,

        /// <summary>
        /// The file replication service cannot populate the system volume because of an internal error.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_SYSVOL_POPULATE = 8013,

        /// <summary>
        /// The file replication service cannot populate the system volume because of an internal timeout.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_SYSVOL_POPULATE_TIMEOUT = 8014,

        /// <summary>
        /// The file replication service cannot process the request. The system volume is busy with a previous request.
        /// </summary>
        FRS_ERR_SYSVOL_IS_BUSY = 8015,

        /// <summary>
        /// The file replication service cannot stop replicating the system volume because of an internal error.
        /// The event log may have more information.
        /// </summary>
        FRS_ERR_SYSVOL_DEMOTE = 8016,

        /// <summary>
        /// The file replication service detected an invalid parameter.
        /// </summary>
        FRS_ERR_INVALID_SERVICE_PARAMETER = 8017,

        /// <summary>
        /// An error occurred while installing the directory service. For more information, see the event log.
        /// </summary>
        DS_NOT_INSTALLED = 8200,

        /// <summary>
        /// The directory service evaluated group memberships locally.
        /// </summary>
        DS_MEMBERSHIP_EVALUATED_LOCALLY = 8201,

        /// <summary>
        /// The specified directory service attribute or value does not exist.
        /// </summary>
        DS_NO_ATTRIBUTE_OR_VALUE = 8202,

        /// <summary>
        /// The attribute syntax specified to the directory service is invalid.
        /// </summary>
        DS_INVALID_ATTRIBUTE_SYNTAX = 8203,

        /// <summary>
        /// The attribute type specified to the directory service is not defined.
        /// </summary>
        DS_ATTRIBUTE_TYPE_UNDEFINED = 8204,

        /// <summary>
        /// The specified directory service attribute or value already exists.
        /// </summary>
        DS_ATTRIBUTE_OR_VALUE_EXISTS = 8205,

        /// <summary>
        /// The directory service is busy.
        /// </summary>
        DS_BUSY = 8206,

        /// <summary>
        /// The directory service is unavailable.
        /// </summary>
        DS_UNAVAILABLE = 8207,

        /// <summary>
        /// The directory service was unable to allocate a relative identifier.
        /// </summary>
        DS_NO_RIDS_ALLOCATED = 8208,

        /// <summary>
        /// The directory service has exhausted the pool of relative identifiers.
        /// </summary>
        DS_NO_MORE_RIDS = 8209,

        /// <summary>
        /// The requested operation could not be performed because the directory service is not the master for that type of operation.
        /// </summary>
        DS_INCORRECT_ROLE_OWNER = 8210,

        /// <summary>
        /// The directory service was unable to initialize the subsystem that allocates relative identifiers.
        /// </summary>
        DS_RIDMGR_INIT_ERROR = 8211,

        /// <summary>
        /// The requested operation did not satisfy one or more constraints associated with the class of the object.
        /// </summary>
        DS_OBJ_CLASS_VIOLATION = 8212,

        /// <summary>
        /// The directory service can perform the requested operation only on a leaf object.
        /// </summary>
        DS_CANT_ON_NON_LEAF = 8213,

        /// <summary>
        /// The directory service cannot perform the requested operation on the RDN attribute of an object.
        /// </summary>
        DS_CANT_ON_RDN = 8214,

        /// <summary>
        /// The directory service detected an attempt to modify the object class of an object.
        /// </summary>
        DS_CANT_MOD_OBJ_CLASS = 8215,

        /// <summary>
        /// The requested cross-domain move operation could not be performed.
        /// </summary>
        DS_CROSS_DOM_MOVE_ERROR = 8216,

        /// <summary>
        /// Unable to contact the global catalog server.
        /// </summary>
        DS_GC_NOT_AVAILABLE = 8217,

        /// <summary>
        /// The policy object is shared and can only be modified at the root.
        /// </summary>
        SHARED_POLICY = 8218,

        /// <summary>
        /// The policy object does not exist.
        /// </summary>
        POLICY_OBJECT_NOT_FOUND = 8219,

        /// <summary>
        /// The requested policy information is only in the directory service.
        /// </summary>
        POLICY_ONLY_IN_DS = 8220,

        /// <summary>
        /// A domain controller promotion is currently active.
        /// </summary>
        PROMOTION_ACTIVE = 8221,

        /// <summary>
        /// A domain controller promotion is not currently active
        /// </summary>
        NO_PROMOTION_ACTIVE = 8222,

        /// <summary>
        /// An operations error occurred.
        /// </summary>
        DS_OPERATIONS_ERROR = 8224,

        /// <summary>
        /// A protocol error occurred.
        /// </summary>
        DS_PROTOCOL_ERROR = 8225,

        /// <summary>
        /// The time limit for this request was exceeded.
        /// </summary>
        DS_TIMELIMIT_EXCEEDED = 8226,

        /// <summary>
        /// The size limit for this request was exceeded.
        /// </summary>
        DS_SIZELIMIT_EXCEEDED = 8227,

        /// <summary>
        /// The administrative limit for this request was exceeded.
        /// </summary>
        DS_ADMIN_LIMIT_EXCEEDED = 8228,

        /// <summary>
        /// The compare response was false.
        /// </summary>
        DS_COMPARE_FALSE = 8229,

        /// <summary>
        /// The compare response was true.
        /// </summary>
        DS_COMPARE_TRUE = 8230,

        /// <summary>
        /// The requested authentication method is not supported by the server.
        /// </summary>
        DS_AUTH_METHOD_NOT_SUPPORTED = 8231,

        /// <summary>
        /// A more secure authentication method is required for this server.
        /// </summary>
        DS_STRONG_AUTH_REQUIRED = 8232,

        /// <summary>
        /// Inappropriate authentication.
        /// </summary>
        DS_INAPPROPRIATE_AUTH = 8233,

        /// <summary>
        /// The authentication mechanism is unknown.
        /// </summary>
        DS_AUTH_UNKNOWN = 8234,

        /// <summary>
        /// A referral was returned from the server.
        /// </summary>
        DS_REFERRAL = 8235,

        /// <summary>
        /// The server does not support the requested critical extension.
        /// </summary>
        DS_UNAVAILABLE_CRIT_EXTENSION = 8236,

        /// <summary>
        /// This request requires a secure connection.
        /// </summary>
        DS_CONFIDENTIALITY_REQUIRED = 8237,

        /// <summary>
        /// Inappropriate matching.
        /// </summary>
        DS_INAPPROPRIATE_MATCHING = 8238,

        /// <summary>
        /// A constraint violation occurred.
        /// </summary>
        DS_CONSTRAINT_VIOLATION = 8239,

        /// <summary>
        /// There is no such object on the server.
        /// </summary>
        DS_NO_SUCH_OBJECT = 8240,

        /// <summary>
        /// There is an alias problem.
        /// </summary>
        DS_ALIAS_PROBLEM = 8241,

        /// <summary>
        /// An invalid dn syntax has been specified.
        /// </summary>
        DS_INVALID_DN_SYNTAX = 8242,

        /// <summary>
        /// The object is a leaf object.
        /// </summary>
        DS_IS_LEAF = 8243,

        /// <summary>
        /// There is an alias dereferencing problem.
        /// </summary>
        DS_ALIAS_DEREF_PROBLEM = 8244,

        /// <summary>
        /// The server is unwilling to process the request.
        /// </summary>
        DS_UNWILLING_TO_PERFORM = 8245,

        /// <summary>
        /// A loop has been detected.
        /// </summary>
        DS_LOOP_DETECT = 8246,

        /// <summary>
        /// There is a naming violation.
        /// </summary>
        DS_NAMING_VIOLATION = 8247,

        /// <summary>
        /// The result set is too large.
        /// </summary>
        DS_OBJECT_RESULTS_TOO_LARGE = 8248,

        /// <summary>
        /// The operation affects multiple DSAs
        /// </summary>
        DS_AFFECTS_MULTIPLE_DSAS = 8249,

        /// <summary>
        /// The server is not operational.
        /// </summary>
        DS_SERVER_DOWN = 8250,

        /// <summary>
        /// A local error has occurred.
        /// </summary>
        DS_LOCAL_ERROR = 8251,

        /// <summary>
        /// An encoding error has occurred.
        /// </summary>
        DS_ENCODING_ERROR = 8252,

        /// <summary>
        /// A decoding error has occurred.
        /// </summary>
        DS_DECODING_ERROR = 8253,

        /// <summary>
        /// The search filter cannot be recognized.
        /// </summary>
        DS_FILTER_UNKNOWN = 8254,

        /// <summary>
        /// One or more parameters are illegal.
        /// </summary>
        DS_PARAM_ERROR = 8255,

        /// <summary>
        /// The specified method is not supported.
        /// </summary>
        DS_NOT_SUPPORTED = 8256,

        /// <summary>
        /// No results were returned.
        /// </summary>
        DS_NO_RESULTS_RETURNED = 8257,

        /// <summary>
        /// The specified control is not supported by the server.
        /// </summary>
        DS_CONTROL_NOT_FOUND = 8258,

        /// <summary>
        /// A referral loop was detected by the client.
        /// </summary>
        DS_CLIENT_LOOP = 8259,

        /// <summary>
        /// The preset referral limit was exceeded.
        /// </summary>
        DS_REFERRAL_LIMIT_EXCEEDED = 8260,

        /// <summary>
        /// The search requires a SORT control.
        /// </summary>
        DS_SORT_CONTROL_MISSING = 8261,

        /// <summary>
        /// The search results exceed the offset range specified.
        /// </summary>
        DS_OFFSET_RANGE_ERROR = 8262,

        /// <summary>
        /// The root object must be the head of a naming context. The root object cannot have an instantiated parent.
        /// </summary>
        DS_ROOT_MUST_BE_NC = 8301,

        /// <summary>
        /// The add replica operation cannot be performed. The naming context must be writable in order to create the replica.
        /// </summary>
        DS_ADD_REPLICA_INHIBITED = 8302,

        /// <summary>
        /// A reference to an attribute that is not defined in the schema occurred.
        /// </summary>
        DS_ATT_NOT_DEF_IN_SCHEMA = 8303,

        /// <summary>
        /// The maximum size of an object has been exceeded.
        /// </summary>
        DS_MAX_OBJ_SIZE_EXCEEDED = 8304,

        /// <summary>
        /// An attempt was made to add an object to the directory with a name that is already in use.
        /// </summary>
        DS_OBJ_STRING_NAME_EXISTS = 8305,

        /// <summary>
        /// An attempt was made to add an object of a class that does not have an RDN defined in the schema.
        /// </summary>
        DS_NO_RDN_DEFINED_IN_SCHEMA = 8306,

        /// <summary>
        /// An attempt was made to add an object using an RDN that is not the RDN defined in the schema.
        /// </summary>
        DS_RDN_DOESNT_MATCH_SCHEMA = 8307,

        /// <summary>
        /// None of the requested attributes were found on the objects.
        /// </summary>
        DS_NO_REQUESTED_ATTS_FOUND = 8308,

        /// <summary>
        /// The user buffer is too small.
        /// </summary>
        DS_USER_BUFFER_TO_SMALL = 8309,

        /// <summary>
        /// The attribute specified in the operation is not present on the object.
        /// </summary>
        DS_ATT_IS_NOT_ON_OBJ = 8310,

        /// <summary>
        /// Illegal modify operation. Some aspect of the modification is not permitted.
        /// </summary>
        DS_ILLEGAL_MOD_OPERATION = 8311,

        /// <summary>
        /// The specified object is too large.
        /// </summary>
        DS_OBJ_TOO_LARGE = 8312,

        /// <summary>
        /// The specified instance type is not valid.
        /// </summary>
        DS_BAD_INSTANCE_TYPE = 8313,

        /// <summary>
        /// The operation must be performed at a master DSA.
        /// </summary>
        DS_MASTERDSA_REQUIRED = 8314,

        /// <summary>
        /// The object class attribute must be specified.
        /// </summary>
        DS_OBJECT_CLASS_REQUIRED = 8315,

        /// <summary>
        /// A required attribute is missing.
        /// </summary>
        DS_MISSING_REQUIRED_ATT = 8316,

        /// <summary>
        /// An attempt was made to modify an object to include an attribute that is not legal for its class.
        /// </summary>
        DS_ATT_NOT_DEF_FOR_CLASS = 8317,

        /// <summary>
        /// The specified attribute is already present on the object.
        /// </summary>
        DS_ATT_ALREADY_EXISTS = 8318,

        /// <summary>
        /// The specified attribute is not present, or has no values.
        /// </summary>
        DS_CANT_ADD_ATT_VALUES = 8320,

        /// <summary>
        /// Mutliple values were specified for an attribute that can have only one value.
        /// </summary>
        DS_SINGLE_VALUE_CONSTRAINT = 8321,

        /// <summary>
        /// A value for the attribute was not in the acceptable range of values.
        /// </summary>
        DS_RANGE_CONSTRAINT = 8322,

        /// <summary>
        /// The specified value already exists.
        /// </summary>
        DS_ATT_VAL_ALREADY_EXISTS = 8323,

        /// <summary>
        /// The attribute cannot be removed because it is not present on the object.
        /// </summary>
        DS_CANT_REM_MISSING_ATT = 8324,

        /// <summary>
        /// The attribute value cannot be removed because it is not present on the object.
        /// </summary>
        DS_CANT_REM_MISSING_ATT_VAL = 8325,

        /// <summary>
        /// The specified root object cannot be a subref.
        /// </summary>
        DS_ROOT_CANT_BE_SUBREF = 8326,

        /// <summary>
        /// Chaining is not permitted.
        /// </summary>
        DS_NO_CHAINING = 8327,

        /// <summary>
        /// Chained evaluation is not permitted.
        /// </summary>
        DS_NO_CHAINED_EVAL = 8328,

        /// <summary>
        /// The operation could not be performed because the object's parent is either uninstantiated or deleted.
        /// </summary>
        DS_NO_PARENT_OBJECT = 8329,

        /// <summary>
        /// Having a parent that is an alias is not permitted. Aliases are leaf objects.
        /// </summary>
        DS_PARENT_IS_AN_ALIAS = 8330,

        /// <summary>
        /// The object and parent must be of the same type, either both masters or both replicas.
        /// </summary>
        DS_CANT_MIX_MASTER_AND_REPS = 8331,

        /// <summary>
        /// The operation cannot be performed because child objects exist. This operation can only be performed on a leaf object.
        /// </summary>
        DS_CHILDREN_EXIST = 8332,

        /// <summary>
        /// Directory object not found.
        /// </summary>
        DS_OBJ_NOT_FOUND = 8333,

        /// <summary>
        /// The aliased object is missing.
        /// </summary>
        DS_ALIASED_OBJ_MISSING = 8334,

        /// <summary>
        /// The object name has bad syntax.
        /// </summary>
        DS_BAD_NAME_SYNTAX = 8335,

        /// <summary>
        /// It is not permitted for an alias to refer to another alias.
        /// </summary>
        DS_ALIAS_POINTS_TO_ALIAS = 8336,

        /// <summary>
        /// The alias cannot be dereferenced.
        /// </summary>
        DS_CANT_DEREF_ALIAS = 8337,

        /// <summary>
        /// The operation is out of scope.
        /// </summary>
        DS_OUT_OF_SCOPE = 8338,

        /// <summary>
        /// The operation cannot continue because the object is in the process of being removed.
        /// </summary>
        DS_OBJECT_BEING_REMOVED = 8339,

        /// <summary>
        /// The DSA object cannot be deleted.
        /// </summary>
        DS_CANT_DELETE_DSA_OBJ = 8340,

        /// <summary>
        /// A directory service error has occurred.
        /// </summary>
        DS_GENERIC_ERROR = 8341,

        /// <summary>
        /// The operation can only be performed on an internal master DSA object.
        /// </summary>
        DS_DSA_MUST_BE_INT_MASTER = 8342,

        /// <summary>
        /// The object must be of class DSA.
        /// </summary>
        DS_CLASS_NOT_DSA = 8343,

        /// <summary>
        /// Insufficient access rights to perform the operation.
        /// </summary>
        DS_INSUFF_ACCESS_RIGHTS = 8344,

        /// <summary>
        /// The object cannot be added because the parent is not on the list of possible superiors.
        /// </summary>
        DS_ILLEGAL_SUPERIOR = 8345,

        /// <summary>
        /// Access to the attribute is not permitted because the attribute is owned by the Security Accounts Manager (SAM).
        /// </summary>
        DS_ATTRIBUTE_OWNED_BY_SAM = 8346,

        /// <summary>
        /// The name has too many parts.
        /// </summary>
        DS_NAME_TOO_MANY_PARTS = 8347,

        /// <summary>
        /// The name is too long.
        /// </summary>
        DS_NAME_TOO_LONG = 8348,

        /// <summary>
        /// The name value is too long.
        /// </summary>
        DS_NAME_VALUE_TOO_LONG = 8349,

        /// <summary>
        /// The directory service encountered an error parsing a name.
        /// </summary>
        DS_NAME_UNPARSEABLE = 8350,

        /// <summary>
        /// The directory service cannot get the attribute type for a name.
        /// </summary>
        DS_NAME_TYPE_UNKNOWN = 8351,

        /// <summary>
        /// The name does not identify an object, the name identifies a phantom.
        /// </summary>
        DS_NOT_AN_OBJECT = 8352,

        /// <summary>
        /// The security descriptor is too short.
        /// </summary>
        DS_SEC_DESC_TOO_LONG = 8353,

        /// <summary>
        /// The security descriptor is invalid.
        /// </summary>
        DS_SEC_DESC_INVALID = 8354,

        /// <summary>
        /// Failed to create name for deleted object.
        /// </summary>
        DS_NO_DELETED_NAME = 8355,

        /// <summary>
        /// The parent of a new subref must exist.
        /// </summary>
        DS_SUBREF_MUST_HAVE_PARENT = 8356,

        /// <summary>
        /// The object must be a naming context.
        /// </summary>
        DS_NCNAME_MUST_BE_NC = 8357,

        /// <summary>
        /// It is not permitted to add an attribute which is owned by the system.
        /// </summary>
        DS_CANT_ADD_SYSTEM_ONLY = 8358,

        /// <summary>
        /// The class of the object must be structural, you cannot instantiate an abstract class.
        /// </summary>
        DS_CLASS_MUST_BE_CONCRETE = 8359,

        /// <summary>
        /// The schema object could not be found.
        /// </summary>
        DS_INVALID_DMD = 8360,

        /// <summary>
        /// A local object with this GUID (dead or alive) already exists.
        /// </summary>
        DS_OBJ_GUID_EXISTS = 8361,

        /// <summary>
        /// The operation cannot be performed on a back link.
        /// </summary>
        DS_NOT_ON_BACKLINK = 8362,

        /// <summary>
        /// The cross reference for the specified naming context could not be found.
        /// </summary>
        DS_NO_CROSSREF_FOR_NC = 8363,

        /// <summary>
        /// The operation could not be performed because the directory service is shutting down.
        /// </summary>
        DS_SHUTTING_DOWN = 8364,

        /// <summary>
        /// The directory service request is invalid.
        /// </summary>
        DS_UNKNOWN_OPERATION = 8365,

        /// <summary>
        /// The role owner attribute could not be read.
        /// </summary>
        DS_INVALID_ROLE_OWNER = 8366,

        /// <summary>
        /// The requested FSMO operation failed. The current FSMO holder could not be contacted.
        /// </summary>
        DS_COULDNT_CONTACT_FSMO = 8367,

        /// <summary>
        /// Modification of a DN across a naming context is not permitted.
        /// </summary>
        DS_CROSS_NC_DN_RENAME = 8368,

        /// <summary>
        /// The attribute cannot be modified because it is owned by the system.
        /// </summary>
        DS_CANT_MOD_SYSTEM_ONLY = 8369,

        /// <summary>
        /// Only the replicator can perform this function.
        /// </summary>
        DS_REPLICATOR_ONLY = 8370,

        /// <summary>
        /// The specified class is not defined.
        /// </summary>
        DS_OBJ_CLASS_NOT_DEFINED = 8371,

        /// <summary>
        /// The specified class is not a subclass.
        /// </summary>
        DS_OBJ_CLASS_NOT_SUBCLASS = 8372,

        /// <summary>
        /// The name reference is invalid.
        /// </summary>
        DS_NAME_REFERENCE_INVALID = 8373,

        /// <summary>
        /// A cross reference already exists.
        /// </summary>
        DS_CROSS_REF_EXISTS = 8374,

        /// <summary>
        /// It is not permitted to delete a master cross reference.
        /// </summary>
        DS_CANT_DEL_MASTER_CROSSREF = 8375,

        /// <summary>
        /// Subtree notifications are only supported on NC heads.
        /// </summary>
        DS_SUBTREE_NOTIFY_NOT_NC_HEAD = 8376,

        /// <summary>
        /// Notification filter is too complex.
        /// </summary>
        DS_NOTIFY_FILTER_TOO_COMPLEX = 8377,

        /// <summary>
        /// Schema update failed: duplicate RDN.
        /// </summary>
        DS_DUP_RDN = 8378,

        /// <summary>
        /// Schema update failed: duplicate OID.
        /// </summary>
        DS_DUP_OID = 8379,

        /// <summary>
        /// Schema update failed: duplicate MAPI identifier.
        /// </summary>
        DS_DUP_MAPI_ID = 8380,

        /// <summary>
        /// Schema update failed: duplicate schema-id GUID.
        /// </summary>
        DS_DUP_SCHEMA_ID_GUID = 8381,

        /// <summary>
        /// Schema update failed: duplicate LDAP display name.
        /// </summary>
        DS_DUP_LDAP_DISPLAY_NAME = 8382,

        /// <summary>
        /// Schema update failed: range-lower less than range upper.
        /// </summary>
        DS_SEMANTIC_ATT_TEST = 8383,

        /// <summary>
        /// Schema update failed: syntax mismatch.
        /// </summary>
        DS_SYNTAX_MISMATCH = 8384,

        /// <summary>
        /// Schema deletion failed: attribute is used in must-contain.
        /// </summary>
        DS_EXISTS_IN_MUST_HAVE = 8385,

        /// <summary>
        /// Schema deletion failed: attribute is used in may-contain.
        /// </summary>
        DS_EXISTS_IN_MAY_HAVE = 8386,

        /// <summary>
        /// Schema update failed: attribute in may-contain does not exist.
        /// </summary>
        DS_NONEXISTENT_MAY_HAVE = 8387,

        /// <summary>
        /// Schema update failed: attribute in must-contain does not exist.
        /// </summary>
        DS_NONEXISTENT_MUST_HAVE = 8388,

        /// <summary>
        /// Schema update failed: class in aux-class list does not exist or is not an auxiliary class.
        /// </summary>
        DS_AUX_CLS_TEST_FAIL = 8389,

        /// <summary>
        /// Schema update failed: class in poss-superiors does not exist.
        /// </summary>
        DS_NONEXISTENT_POSS_SUP = 8390,

        /// <summary>
        /// Schema update failed: class in subclassof list does not exist or does not satisfy hierarchy rules.
        /// </summary>
        DS_SUB_CLS_TEST_FAIL = 8391,

        /// <summary>
        /// Schema update failed: Rdn-Att-Id has wrong syntax.
        /// </summary>
        DS_BAD_RDN_ATT_ID_SYNTAX = 8392,

        /// <summary>
        /// Schema deletion failed: class is used as auxiliary class.
        /// </summary>
        DS_EXISTS_IN_AUX_CLS = 8393,

        /// <summary>
        /// Schema deletion failed: class is used as sub class.
        /// </summary>
        DS_EXISTS_IN_SUB_CLS = 8394,

        /// <summary>
        /// Schema deletion failed: class is used as poss superior.
        /// </summary>
        DS_EXISTS_IN_POSS_SUP = 8395,

        /// <summary>
        /// Schema update failed in recalculating validation cache.
        /// </summary>
        DS_RECALCSCHEMA_FAILED = 8396,

        /// <summary>
        /// The tree deletion is not finished.  The request must be made again to continue deleting the tree.
        /// </summary>
        DS_TREE_DELETE_NOT_FINISHED = 8397,

        /// <summary>
        /// The requested delete operation could not be performed.
        /// </summary>
        DS_CANT_DELETE = 8398,

        /// <summary>
        /// Cannot read the governs class identifier for the schema record.
        /// </summary>
        DS_ATT_SCHEMA_REQ_ID = 8399,

        /// <summary>
        /// The attribute schema has bad syntax.
        /// </summary>
        DS_BAD_ATT_SCHEMA_SYNTAX = 8400,

        /// <summary>
        /// The attribute could not be cached.
        /// </summary>
        DS_CANT_CACHE_ATT = 8401,

        /// <summary>
        /// The class could not be cached.
        /// </summary>
        DS_CANT_CACHE_CLASS = 8402,

        /// <summary>
        /// The attribute could not be removed from the cache.
        /// </summary>
        DS_CANT_REMOVE_ATT_CACHE = 8403,

        /// <summary>
        /// The class could not be removed from the cache.
        /// </summary>
        DS_CANT_REMOVE_CLASS_CACHE = 8404,

        /// <summary>
        /// The distinguished name attribute could not be read.
        /// </summary>
        DS_CANT_RETRIEVE_DN = 8405,

        /// <summary>
        /// A required subref is missing.
        /// </summary>
        DS_MISSING_SUPREF = 8406,

        /// <summary>
        /// The instance type attribute could not be retrieved.
        /// </summary>
        DS_CANT_RETRIEVE_INSTANCE = 8407,

        /// <summary>
        /// An internal error has occurred.
        /// </summary>
        DS_CODE_INCONSISTENCY = 8408,

        /// <summary>
        /// A database error has occurred.
        /// </summary>
        DS_DATABASE_ERROR = 8409,

        /// <summary>
        /// The attribute GOVERNSID is missing.
        /// </summary>
        DS_GOVERNSID_MISSING = 8410,

        /// <summary>
        /// An expected attribute is missing.
        /// </summary>
        DS_MISSING_EXPECTED_ATT = 8411,

        /// <summary>
        /// The specified naming context is missing a cross reference.
        /// </summary>
        DS_NCNAME_MISSING_CR_REF = 8412,

        /// <summary>
        /// A security checking error has occurred.
        /// </summary>
        DS_SECURITY_CHECKING_ERROR = 8413,

        /// <summary>
        /// The schema is not loaded.
        /// </summary>
        DS_SCHEMA_NOT_LOADED = 8414,

        /// <summary>
        /// Schema allocation failed. Please check if the machine is running low on memory.
        /// </summary>
        DS_SCHEMA_ALLOC_FAILED = 8415,

        /// <summary>
        /// Failed to obtain the required syntax for the attribute schema.
        /// </summary>
        DS_ATT_SCHEMA_REQ_SYNTAX = 8416,

        /// <summary>
        /// The global catalog verification failed. The global catalog is not available or does not support the operation. Some part of the directory is currently not available.
        /// </summary>
        DS_GCVERIFY_ERROR = 8417,

        /// <summary>
        /// The replication operation failed because of a schema mismatch between the servers involved.
        /// </summary>
        DS_DRA_SCHEMA_MISMATCH = 8418,

        /// <summary>
        /// The DSA object could not be found.
        /// </summary>
        DS_CANT_FIND_DSA_OBJ = 8419,

        /// <summary>
        /// The naming context could not be found.
        /// </summary>
        DS_CANT_FIND_EXPECTED_NC = 8420,

        /// <summary>
        /// The naming context could not be found in the cache.
        /// </summary>
        DS_CANT_FIND_NC_IN_CACHE = 8421,

        /// <summary>
        /// The child object could not be retrieved.
        /// </summary>
        DS_CANT_RETRIEVE_CHILD = 8422,

        /// <summary>
        /// The modification was not permitted for security reasons.
        /// </summary>
        DS_SECURITY_ILLEGAL_MODIFY = 8423,

        /// <summary>
        /// The operation cannot replace the hidden record.
        /// </summary>
        DS_CANT_REPLACE_HIDDEN_REC = 8424,

        /// <summary>
        /// The hierarchy file is invalid.
        /// </summary>
        DS_BAD_HIERARCHY_FILE = 8425,

        /// <summary>
        /// The attempt to build the hierarchy table failed.
        /// </summary>
        DS_BUILD_HIERARCHY_TABLE_FAILED = 8426,

        /// <summary>
        /// The directory configuration parameter is missing from the registry.
        /// </summary>
        DS_CONFIG_PARAM_MISSING = 8427,

        /// <summary>
        /// The attempt to count the address book indices failed.
        /// </summary>
        DS_COUNTING_AB_INDICES_FAILED = 8428,

        /// <summary>
        /// The allocation of the hierarchy table failed.
        /// </summary>
        DS_HIERARCHY_TABLE_MALLOC_FAILED = 8429,

        /// <summary>
        /// The directory service encountered an internal failure.
        /// </summary>
        DS_INTERNAL_FAILURE = 8430,

        /// <summary>
        /// The directory service encountered an unknown failure.
        /// </summary>
        DS_UNKNOWN_ERROR = 8431,

        /// <summary>
        /// A root object requires a class of 'top'.
        /// </summary>
        DS_ROOT_REQUIRES_CLASS_TOP = 8432,

        /// <summary>
        /// This directory server is shutting down, and cannot take ownership of new floating single-master operation roles.
        /// </summary>
        DS_REFUSING_FSMO_ROLES = 8433,

        /// <summary>
        /// The directory service is missing mandatory configuration information, and is unable to determine the ownership of floating single-master operation roles.
        /// </summary>
        DS_MISSING_FSMO_SETTINGS = 8434,

        /// <summary>
        /// The directory service was unable to transfer ownership of one or more floating single-master operation roles to other servers.
        /// </summary>
        DS_UNABLE_TO_SURRENDER_ROLES = 8435,

        /// <summary>
        /// The replication operation failed.
        /// </summary>
        DS_DRA_GENERIC = 8436,

        /// <summary>
        /// An invalid parameter was specified for this replication operation.
        /// </summary>
        DS_DRA_INVALID_PARAMETER = 8437,

        /// <summary>
        /// The directory service is too busy to complete the replication operation at this time.
        /// </summary>
        DS_DRA_BUSY = 8438,

        /// <summary>
        /// The distinguished name specified for this replication operation is invalid.
        /// </summary>
        DS_DRA_BAD_DN = 8439,

        /// <summary>
        /// The naming context specified for this replication operation is invalid.
        /// </summary>
        DS_DRA_BAD_NC = 8440,

        /// <summary>
        /// The distinguished name specified for this replication operation already exists.
        /// </summary>
        DS_DRA_DN_EXISTS = 8441,

        /// <summary>
        /// The replication system encountered an internal error.
        /// </summary>
        DS_DRA_INTERNAL_ERROR = 8442,

        /// <summary>
        /// The replication operation encountered a database inconsistency.
        /// </summary>
        DS_DRA_INCONSISTENT_DIT = 8443,

        /// <summary>
        /// The server specified for this replication operation could not be contacted.
        /// </summary>
        DS_DRA_CONNECTION_FAILED = 8444,

        /// <summary>
        /// The replication operation encountered an object with an invalid instance type.
        /// </summary>
        DS_DRA_BAD_INSTANCE_TYPE = 8445,

        /// <summary>
        /// The replication operation failed to allocate memory.
        /// </summary>
        DS_DRA_OUT_OF_MEM = 8446,

        /// <summary>
        /// The replication operation encountered an error with the mail system.
        /// </summary>
        DS_DRA_MAIL_PROBLEM = 8447,

        /// <summary>
        /// The replication reference information for the target server already exists.
        /// </summary>
        DS_DRA_REF_ALREADY_EXISTS = 8448,

        /// <summary>
        /// The replication reference information for the target server does not exist.
        /// </summary>
        DS_DRA_REF_NOT_FOUND = 8449,

        /// <summary>
        /// The naming context cannot be removed because it is replicated to another server.
        /// </summary>
        DS_DRA_OBJ_IS_REP_SOURCE = 8450,

        /// <summary>
        /// The replication operation encountered a database error.
        /// </summary>
        DS_DRA_DB_ERROR = 8451,

        /// <summary>
        /// The naming context is in the process of being removed or is not replicated from the specified server.
        /// </summary>
        DS_DRA_NO_REPLICA = 8452,

        /// <summary>
        /// Replication access was denied.
        /// </summary>
        DS_DRA_ACCESS_DENIED = 8453,

        /// <summary>
        /// The requested operation is not supported by this version of the directory service.
        /// </summary>
        DS_DRA_NOT_SUPPORTED = 8454,

        /// <summary>
        /// The replication remote procedure call was cancelled.
        /// </summary>
        DS_DRA_RPC_CANCELLED = 8455,

        /// <summary>
        /// The source server is currently rejecting replication requests.
        /// </summary>
        DS_DRA_SOURCE_DISABLED = 8456,

        /// <summary>
        /// The destination server is currently rejecting replication requests.
        /// </summary>
        DS_DRA_SINK_DISABLED = 8457,

        /// <summary>
        /// The replication operation failed due to a collision of object names.
        /// </summary>
        DS_DRA_NAME_COLLISION = 8458,

        /// <summary>
        /// The replication source has been reinstalled.
        /// </summary>
        DS_DRA_SOURCE_REINSTALLED = 8459,

        /// <summary>
        /// The replication operation failed because a required parent object is missing.
        /// </summary>
        DS_DRA_MISSING_PARENT = 8460,

        /// <summary>
        /// The replication operation was preempted.
        /// </summary>
        DS_DRA_PREEMPTED = 8461,

        /// <summary>
        /// The replication synchronization attempt was abandoned because of a lack of updates.
        /// </summary>
        DS_DRA_ABANDON_SYNC = 8462,

        /// <summary>
        /// The replication operation was terminated because the system is shutting down.
        /// </summary>
        DS_DRA_SHUTDOWN = 8463,

        /// <summary>
        /// The replication synchronization attempt failed as the destination partial attribute set is not a subset of source partial attribute set.
        /// </summary>
        DS_DRA_INCOMPATIBLE_PARTIAL_SET = 8464,

        /// <summary>
        /// The replication synchronization attempt failed because a master replica attempted to sync from a partial replica.
        /// </summary>
        DS_DRA_SOURCE_IS_PARTIAL_REPLICA = 8465,

        /// <summary>
        /// The server specified for this replication operation was contacted, but that server was unable to contact an additional server needed to complete the operation.
        /// </summary>
        DS_DRA_EXTN_CONNECTION_FAILED = 8466,

        /// <summary>
        /// The version of the Active Directory schema of the source forest is not compatible with the version of Active Directory on this computer.  You must upgrade the operating system on a domain controller in the source forest before this computer can be added as a domain controller to that forest.
        /// </summary>
        DS_INSTALL_SCHEMA_MISMATCH = 8467,

        /// <summary>
        /// Schema update failed: An attribute with the same link identifier already exists.
        /// </summary>
        DS_DUP_LINK_ID = 8468,

        /// <summary>
        /// Name translation: Generic processing error.
        /// </summary>
        DS_NAME_ERROR_RESOLVING = 8469,

        /// <summary>
        /// Name translation: Could not find the name or insufficient right to see name.
        /// </summary>
        DS_NAME_ERROR_NOT_FOUND = 8470,

        /// <summary>
        /// Name translation: Input name mapped to more than one output name.
        /// </summary>
        DS_NAME_ERROR_NOT_UNIQUE = 8471,

        /// <summary>
        /// Name translation: Input name found, but not the associated output format.
        /// </summary>
        DS_NAME_ERROR_NO_MAPPING = 8472,

        /// <summary>
        /// Name translation: Unable to resolve completely, only the domain was found.
        /// </summary>
        DS_NAME_ERROR_DOMAIN_ONLY = 8473,

        /// <summary>
        /// Name translation: Unable to perform purely syntactical mapping at the client without going out to the wire.
        /// </summary>
        DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING = 8474,

        /// <summary>
        /// Modification of a constructed att is not allowed.
        /// </summary>
        DS_CONSTRUCTED_ATT_MOD = 8475,

        /// <summary>
        /// The OM-Object-Class specified is incorrect for an attribute with the specified syntax.
        /// </summary>
        DS_WRONG_OM_OBJ_CLASS = 8476,

        /// <summary>
        /// The replication request has been posted, waiting for reply.
        /// </summary>
        DS_DRA_REPL_PENDING = 8477,

        /// <summary>
        /// The requested operation requires a directory service, and none was available.
        /// </summary>
        DS_DS_REQUIRED = 8478,

        /// <summary>
        /// The LDAP display name of the class or attribute contains non-ASCII characters.
        /// </summary>
        DS_INVALID_LDAP_DISPLAY_NAME = 8479,

        /// <summary>
        /// The requested search operation is only supported for base searches.
        /// </summary>
        DS_NON_BASE_SEARCH = 8480,

        /// <summary>
        /// The search failed to retrieve attributes from the database.
        /// </summary>
        DS_CANT_RETRIEVE_ATTS = 8481,

        /// <summary>
        /// The schema update operation tried to add a backward link attribute that has no corresponding forward link.
        /// </summary>
        DS_BACKLINK_WITHOUT_LINK = 8482,

        /// <summary>
        /// Source and destination of a cross-domain move do not agree on the object's epoch number.  Either source or destination does not have the latest version of the object.
        /// </summary>
        DS_EPOCH_MISMATCH = 8483,

        /// <summary>
        /// Source and destination of a cross-domain move do not agree on the object's current name.  Either source or destination does not have the latest version of the object.
        /// </summary>
        DS_SRC_NAME_MISMATCH = 8484,

        /// <summary>
        /// Source and destination for the cross-domain move operation are identical.  Caller should use local move operation instead of cross-domain move operation.
        /// </summary>
        DS_SRC_AND_DST_NC_IDENTICAL = 8485,

        /// <summary>
        /// Source and destination for a cross-domain move are not in agreement on the naming contexts in the forest.  Either source or destination does not have the latest version of the Partitions container.
        /// </summary>
        DS_DST_NC_MISMATCH = 8486,

        /// <summary>
        /// Destination of a cross-domain move is not authoritative for the destination naming context.
        /// </summary>
        DS_NOT_AUTHORITIVE_FOR_DST_NC = 8487,

        /// <summary>
        /// Source and destination of a cross-domain move do not agree on the identity of the source object.  Either source or destination does not have the latest version of the source object.
        /// </summary>
        DS_SRC_GUID_MISMATCH = 8488,

        /// <summary>
        /// Object being moved across-domains is already known to be deleted by the destination server.  The source server does not have the latest version of the source object.
        /// </summary>
        DS_CANT_MOVE_DELETED_OBJECT = 8489,

        /// <summary>
        /// Another operation which requires exclusive access to the PDC FSMO is already in progress.
        /// </summary>
        DS_PDC_OPERATION_IN_PROGRESS = 8490,

        /// <summary>
        /// A cross-domain move operation failed such that two versions of the moved object exist - one each in the source and destination domains.  The destination object needs to be removed to restore the system to a consistent state.
        /// </summary>
        DS_CROSS_DOMAIN_CLEANUP_REQD = 8491,

        /// <summary>
        /// This object may not be moved across domain boundaries either because cross-domain moves for this class are disallowed, or the object has some special characteristics, eg: trust account or restricted RID, which prevent its move.
        /// </summary>
        DS_ILLEGAL_XDOM_MOVE_OPERATION = 8492,

        /// <summary>
        /// Can't move objects with memberships across domain boundaries as once moved, this would violate the membership conditions of the account group.  Remove the object from any account group memberships and retry.
        /// </summary>
        DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS = 8493,

        /// <summary>
        /// A naming context head must be the immediate child of another naming context head, not of an interior node.
        /// </summary>
        DS_NC_MUST_HAVE_NC_PARENT = 8494,

        /// <summary>
        /// The directory cannot validate the proposed naming context name because it does not hold a replica of the naming context above the proposed naming context.  Please ensure that the domain naming master role is held by a server that is configured as a global catalog server, and that the server is up to date with its replication partners. (Applies only to Windows 2000 Domain Naming masters)
        /// </summary>
        DS_CR_IMPOSSIBLE_TO_VALIDATE = 8495,

        /// <summary>
        /// Destination domain must be in native mode.
        /// </summary>
        DS_DST_DOMAIN_NOT_NATIVE = 8496,

        /// <summary>
        /// The operation can not be performed because the server does not have an infrastructure container in the domain of interest.
        /// </summary>
        DS_MISSING_INFRASTRUCTURE_CONTAINER = 8497,

        /// <summary>
        /// Cross-domain move of non-empty account groups is not allowed.
        /// </summary>
        DS_CANT_MOVE_ACCOUNT_GROUP = 8498,

        /// <summary>
        /// Cross-domain move of non-empty resource groups is not allowed.
        /// </summary>
        DS_CANT_MOVE_RESOURCE_GROUP = 8499,

        /// <summary>
        /// The search flags for the attribute are invalid. The ANR bit is valid only on attributes of Unicode or Teletex strings.
        /// </summary>
        DS_INVALID_SEARCH_FLAG = 8500,

        /// <summary>
        /// Tree deletions starting at an object which has an NC head as a descendant are not allowed.
        /// </summary>
        DS_NO_TREE_DELETE_ABOVE_NC = 8501,

        /// <summary>
        /// The directory service failed to lock a tree in preparation for a tree deletion because the tree was in use.
        /// </summary>
        DS_COULDNT_LOCK_TREE_FOR_DELETE = 8502,

        /// <summary>
        /// The directory service failed to identify the list of objects to delete while attempting a tree deletion.
        /// </summary>
        DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE = 8503,

        /// <summary>
        /// Security Accounts Manager initialization failed because of the following error: %1.
        /// Error Status: 0x%2. Click OK to shut down the system and reboot into Directory Services Restore Mode. Check the event log for detailed information.
        /// </summary>
        DS_SAM_INIT_FAILURE = 8504,

        /// <summary>
        /// Only an administrator can modify the membership list of an administrative group.
        /// </summary>
        DS_SENSITIVE_GROUP_VIOLATION = 8505,

        /// <summary>
        /// Cannot change the primary group ID of a domain controller account.
        /// </summary>
        DS_CANT_MOD_PRIMARYGROUPID = 8506,

        /// <summary>
        /// An attempt is made to modify the base schema.
        /// </summary>
        DS_ILLEGAL_BASE_SCHEMA_MOD = 8507,

        /// <summary>
        /// Adding a new mandatory attribute to an existing class, deleting a mandatory attribute from an existing class, or adding an optional attribute to the special class Top that is not a backlink attribute (directly or through inheritance, for example, by adding or deleting an auxiliary class) is not allowed.
        /// </summary>
        DS_NONSAFE_SCHEMA_CHANGE = 8508,

        /// <summary>
        /// Schema update is not allowed on this DC because the DC is not the schema FSMO Role Owner.
        /// </summary>
        DS_SCHEMA_UPDATE_DISALLOWED = 8509,

        /// <summary>
        /// An object of this class cannot be created under the schema container. You can only create attribute-schema and class-schema objects under the schema container.
        /// </summary>
        DS_CANT_CREATE_UNDER_SCHEMA = 8510,

        /// <summary>
        /// The replica/child install failed to get the objectVersion attribute on the schema container on the source DC. Either the attribute is missing on the schema container or the credentials supplied do not have permission to read it.
        /// </summary>
        DS_INSTALL_NO_SRC_SCH_VERSION = 8511,

        /// <summary>
        /// The replica/child install failed to read the objectVersion attribute in the SCHEMA section of the file schema.ini in the system32 directory.
        /// </summary>
        DS_INSTALL_NO_SCH_VERSION_IN_INIFILE = 8512,

        /// <summary>
        /// The specified group type is invalid.
        /// </summary>
        DS_INVALID_GROUP_TYPE = 8513,

        /// <summary>
        /// You cannot nest global groups in a mixed domain if the group is security-enabled.
        /// </summary>
        DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = 8514,

        /// <summary>
        /// You cannot nest local groups in a mixed domain if the group is security-enabled.
        /// </summary>
        DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = 8515,

        /// <summary>
        /// A global group cannot have a local group as a member.
        /// </summary>
        DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER = 8516,

        /// <summary>
        /// A global group cannot have a universal group as a member.
        /// </summary>
        DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER = 8517,

        /// <summary>
        /// A universal group cannot have a local group as a member.
        /// </summary>
        DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER = 8518,

        /// <summary>
        /// A global group cannot have a cross-domain member.
        /// </summary>
        DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER = 8519,

        /// <summary>
        /// A local group cannot have another cross domain local group as a member.
        /// </summary>
        DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER = 8520,

        /// <summary>
        /// A group with primary members cannot change to a security-disabled group.
        /// </summary>
        DS_HAVE_PRIMARY_MEMBERS = 8521,

        /// <summary>
        /// The schema cache load failed to convert the string default SD on a class-schema object.
        /// </summary>
        DS_STRING_SD_CONVERSION_FAILED = 8522,

        /// <summary>
        /// Only DSAs configured to be Global Catalog servers should be allowed to hold the Domain Naming Master FSMO role. (Applies only to Windows 2000 servers)
        /// </summary>
        DS_NAMING_MASTER_GC = 8523,

        /// <summary>
        /// The DSA operation is unable to proceed because of a DNS lookup failure.
        /// </summary>
        DS_DNS_LOOKUP_FAILURE = 8524,

        /// <summary>
        /// While processing a change to the DNS Host Name for an object, the Service Principal Name values could not be kept in sync.
        /// </summary>
        DS_COULDNT_UPDATE_SPNS = 8525,

        /// <summary>
        /// The Security Descriptor attribute could not be read.
        /// </summary>
        DS_CANT_RETRIEVE_SD = 8526,

        /// <summary>
        /// The object requested was not found, but an object with that key was found.
        /// </summary>
        DS_KEY_NOT_UNIQUE = 8527,

        /// <summary>
        /// The syntax of the linked attribute being added is incorrect. Forward links can only have syntax 2.5.5.1, 2.5.5.7, and 2.5.5.14, and backlinks can only have syntax 2.5.5.1
        /// </summary>
        DS_WRONG_LINKED_ATT_SYNTAX = 8528,

        /// <summary>
        /// Security Account Manager needs to get the boot password.
        /// </summary>
        DS_SAM_NEED_BOOTKEY_PASSWORD = 8529,

        /// <summary>
        /// Security Account Manager needs to get the boot key from floppy disk.
        /// </summary>
        DS_SAM_NEED_BOOTKEY_FLOPPY = 8530,

        /// <summary>
        /// Directory Service cannot start.
        /// </summary>
        DS_CANT_START = 8531,

        /// <summary>
        /// Directory Services could not start.
        /// </summary>
        DS_INIT_FAILURE = 8532,

        /// <summary>
        /// The connection between client and server requires packet privacy or better.
        /// </summary>
        DS_NO_PKT_PRIVACY_ON_CONNECTION = 8533,

        /// <summary>
        /// The source domain may not be in the same forest as destination.
        /// </summary>
        DS_SOURCE_DOMAIN_IN_FOREST = 8534,

        /// <summary>
        /// The destination domain must be in the forest.
        /// </summary>
        DS_DESTINATION_DOMAIN_NOT_IN_FOREST = 8535,

        /// <summary>
        /// The operation requires that destination domain auditing be enabled.
        /// </summary>
        DS_DESTINATION_AUDITING_NOT_ENABLED = 8536,

        /// <summary>
        /// The operation couldn't locate a DC for the source domain.
        /// </summary>
        DS_CANT_FIND_DC_FOR_SRC_DOMAIN = 8537,

        /// <summary>
        /// The source object must be a group or user.
        /// </summary>
        DS_SRC_OBJ_NOT_GROUP_OR_USER = 8538,

        /// <summary>
        /// The source object's SID already exists in destination forest.
        /// </summary>
        DS_SRC_SID_EXISTS_IN_FOREST = 8539,

        /// <summary>
        /// The source and destination object must be of the same type.
        /// </summary>
        DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH = 8540,

        /// <summary>
        /// Security Accounts Manager initialization failed because of the following error: %1.
        /// Error Status: 0x%2. Click OK to shut down the system and reboot into Safe Mode. Check the event log for detailed information.
        /// </summary>
        SAM_INIT_FAILURE = 8541,

        /// <summary>
        /// Schema information could not be included in the replication request.
        /// </summary>
        DS_DRA_SCHEMA_INFO_SHIP = 8542,

        /// <summary>
        /// The replication operation could not be completed due to a schema incompatibility.
        /// </summary>
        DS_DRA_SCHEMA_CONFLICT = 8543,

        /// <summary>
        /// The replication operation could not be completed due to a previous schema incompatibility.
        /// </summary>
        DS_DRA_EARLIER_SCHEMA_CONFLICT = 8544,

        /// <summary>
        /// The replication update could not be applied because either the source or the destination has not yet received information regarding a recent cross-domain move operation.
        /// </summary>
        DS_DRA_OBJ_NC_MISMATCH = 8545,

        /// <summary>
        /// The requested domain could not be deleted because there exist domain controllers that still host this domain.
        /// </summary>
        DS_NC_STILL_HAS_DSAS = 8546,

        /// <summary>
        /// The requested operation can be performed only on a global catalog server.
        /// </summary>
        DS_GC_REQUIRED = 8547,

        /// <summary>
        /// A local group can only be a member of other local groups in the same domain.
        /// </summary>
        DS_LOCAL_MEMBER_OF_LOCAL_ONLY = 8548,

        /// <summary>
        /// Foreign security principals cannot be members of universal groups.
        /// </summary>
        DS_NO_FPO_IN_UNIVERSAL_GROUPS = 8549,

        /// <summary>
        /// The attribute is not allowed to be replicated to the GC because of security reasons.
        /// </summary>
        DS_CANT_ADD_TO_GC = 8550,

        /// <summary>
        /// The checkpoint with the PDC could not be taken because there too many modifications being processed currently.
        /// </summary>
        DS_NO_CHECKPOINT_WITH_PDC = 8551,

        /// <summary>
        /// The operation requires that source domain auditing be enabled.
        /// </summary>
        DS_SOURCE_AUDITING_NOT_ENABLED = 8552,

        /// <summary>
        /// Security principal objects can only be created inside domain naming contexts.
        /// </summary>
        DS_CANT_CREATE_IN_NONDOMAIN_NC = 8553,

        /// <summary>
        /// A Service Principal Name (SPN) could not be constructed because the provided hostname is not in the necessary format.
        /// </summary>
        DS_INVALID_NAME_FOR_SPN = 8554,

        /// <summary>
        /// A Filter was passed that uses constructed attributes.
        /// </summary>
        DS_FILTER_USES_CONTRUCTED_ATTRS = 8555,

        /// <summary>
        /// The unicodePwd attribute value must be enclosed in double quotes.
        /// </summary>
        DS_UNICODEPWD_NOT_IN_QUOTES = 8556,

        /// <summary>
        /// Your computer could not be joined to the domain. You have exceeded the maximum number of computer accounts you are allowed to create in this domain. Contact your system administrator to have this limit reset or increased.
        /// </summary>
        DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED = 8557,

        /// <summary>
        /// For security reasons, the operation must be run on the destination DC.
        /// </summary>
        DS_MUST_BE_RUN_ON_DST_DC = 8558,

        /// <summary>
        /// For security reasons, the source DC must be NT4SP4 or greater.
        /// </summary>
        DS_SRC_DC_MUST_BE_SP4_OR_GREATER = 8559,

        /// <summary>
        /// Critical Directory Service System objects cannot be deleted during tree delete operations.  The tree delete may have been partially performed.
        /// </summary>
        DS_CANT_TREE_DELETE_CRITICAL_OBJ = 8560,

        /// <summary>
        /// Directory Services could not start because of the following error: %1.
        /// Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.
        /// </summary>
        DS_INIT_FAILURE_CONSOLE = 8561,

        /// <summary>
        /// Security Accounts Manager initialization failed because of the following error: %1.
        /// Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.
        /// </summary>
        DS_SAM_INIT_FAILURE_CONSOLE = 8562,

        /// <summary>
        /// This version of Windows is too old to support the current directory forest behavior.  You must upgrade the operating system on this server before it can become a domain controller in this forest.
        /// </summary>
        DS_FOREST_VERSION_TOO_HIGH = 8563,

        /// <summary>
        /// This version of Windows is too old to support the current domain behavior.  You must upgrade the operating system on this server before it can become a domain controller in this domain.
        /// </summary>
        DS_DOMAIN_VERSION_TOO_HIGH = 8564,

        /// <summary>
        /// This version of Windows no longer supports the behavior version in use in this directory forest.  You must advance the forest behavior version before this server can become a domain controller in the forest.
        /// </summary>
        DS_FOREST_VERSION_TOO_LOW = 8565,

        /// <summary>
        /// This version of Windows no longer supports the behavior version in use in this domain.  You must advance the domain behavior version before this server can become a domain controller in the domain.
        /// </summary>
        DS_DOMAIN_VERSION_TOO_LOW = 8566,

        /// <summary>
        /// The version of Windows is incompatible with the behavior version of the domain or forest.
        /// </summary>
        DS_INCOMPATIBLE_VERSION = 8567,

        /// <summary>
        /// The behavior version cannot be increased to the requested value because Domain Controllers still exist with versions lower than the requested value.
        /// </summary>
        DS_LOW_DSA_VERSION = 8568,

        /// <summary>
        /// The behavior version value cannot be increased while the domain is still in mixed domain mode.  You must first change the domain to native mode before increasing the behavior version.
        /// </summary>
        DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN = 8569,

        /// <summary>
        /// The sort order requested is not supported.
        /// </summary>
        DS_NOT_SUPPORTED_SORT_ORDER = 8570,

        /// <summary>
        /// Found an object with a non unique name.
        /// </summary>
        DS_NAME_NOT_UNIQUE = 8571,

        /// <summary>
        /// The machine account was created pre-NT4.  The account needs to be recreated.
        /// </summary>
        DS_MACHINE_ACCOUNT_CREATED_PRENT4 = 8572,

        /// <summary>
        /// The database is out of version store.
        /// </summary>
        DS_OUT_OF_VERSION_STORE = 8573,

        /// <summary>
        /// Unable to continue operation because multiple conflicting controls were used.
        /// </summary>
        DS_INCOMPATIBLE_CONTROLS_USED = 8574,

        /// <summary>
        /// Unable to find a valid security descriptor reference domain for this partition.
        /// </summary>
        DS_NO_REF_DOMAIN = 8575,

        /// <summary>
        /// Schema update failed: The link identifier is reserved.
        /// </summary>
        DS_RESERVED_LINK_ID = 8576,

        /// <summary>
        /// Schema update failed: There are no link identifiers available.
        /// </summary>
        DS_LINK_ID_NOT_AVAILABLE = 8577,

        /// <summary>
        /// A account group can not have a universal group as a member.
        /// </summary>
        DS_AG_CANT_HAVE_UNIVERSAL_MEMBER = 8578,

        /// <summary>
        /// Rename or move operations on naming context heads or read-only objects are not allowed.
        /// </summary>
        DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE = 8579,

        /// <summary>
        /// Move operations on objects in the schema naming context are not allowed.
        /// </summary>
        DS_NO_OBJECT_MOVE_IN_SCHEMA_NC = 8580,

        /// <summary>
        /// A system flag has been set on the object and does not allow the object to be moved or renamed.
        /// </summary>
        DS_MODIFYDN_DISALLOWED_BY_FLAG = 8581,

        /// <summary>
        /// This object is not allowed to change its grandparent container. Moves are not forbidden on this object, but are restricted to sibling containers.
        /// </summary>
        DS_MODIFYDN_WRONG_GRANDPARENT = 8582,

        /// <summary>
        /// Unable to resolve completely, a referral to another forest is generated.
        /// </summary>
        DS_NAME_ERROR_TRUST_REFERRAL = 8583,

        /// <summary>
        /// The requested action is not supported on standard server.
        /// </summary>
        NOT_SUPPORTED_ON_STANDARD_SERVER = 8584,

        /// <summary>
        /// Could not access a partition of the Active Directory located on a remote server.  Make sure at least one server is running for the partition in question.
        /// </summary>
        DS_CANT_ACCESS_REMOTE_PART_OF_AD = 8585,

        /// <summary>
        /// The directory cannot validate the proposed naming context (or partition) name because it does not hold a replica nor can it contact a replica of the naming context above the proposed naming context.  Please ensure that the parent naming context is properly registered in DNS, and at least one replica of this naming context is reachable by the Domain Naming master.
        /// </summary>
        DS_CR_IMPOSSIBLE_TO_VALIDATE_V2 = 8586,

        /// <summary>
        /// The thread limit for this request was exceeded.
        /// </summary>
        DS_THREAD_LIMIT_EXCEEDED = 8587,

        /// <summary>
        /// The Global catalog server is not in the closest site.
        /// </summary>
        DS_NOT_CLOSEST = 8588,

        /// <summary>
        /// The DS cannot derive a service principal name (SPN) with which to mutually authenticate the target server because the corresponding server object in the local DS database has no serverReference attribute.
        /// </summary>
        DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF = 8589,

        /// <summary>
        /// The Directory Service failed to enter single user mode.
        /// </summary>
        DS_SINGLE_USER_MODE_FAILED = 8590,

        /// <summary>
        /// The Directory Service cannot parse the script because of a syntax error.
        /// </summary>
        DS_NTDSCRIPT_SYNTAX_ERROR = 8591,

        /// <summary>
        /// The Directory Service cannot process the script because of an error.
        /// </summary>
        DS_NTDSCRIPT_PROCESS_ERROR = 8592,

        /// <summary>
        /// The directory service cannot perform the requested operation because the servers
        /// involved are of different replication epochs (which is usually related to a
        /// domain rename that is in progress).
        /// </summary>
        DS_DIFFERENT_REPL_EPOCHS = 8593,

        /// <summary>
        /// The directory service binding must be renegotiated due to a change in the server
        /// extensions information.
        /// </summary>
        DS_DRS_EXTENSIONS_CHANGED = 8594,

        /// <summary>
        /// Operation not allowed on a disabled cross ref.
        /// </summary>
        DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR = 8595,

        /// <summary>
        /// Schema update failed: No values for msDS-IntId are available.
        /// </summary>
        DS_NO_MSDS_INTID = 8596,

        /// <summary>
        /// Schema update failed: Duplicate msDS-INtId. Retry the operation.
        /// </summary>
        DS_DUP_MSDS_INTID = 8597,

        /// <summary>
        /// Schema deletion failed: attribute is used in rDNAttID.
        /// </summary>
        DS_EXISTS_IN_RDNATTID = 8598,

        /// <summary>
        /// The directory service failed to authorize the request.
        /// </summary>
        DS_AUTHORIZATION_FAILED = 8599,

        /// <summary>
        /// The Directory Service cannot process the script because it is invalid.
        /// </summary>
        DS_INVALID_SCRIPT = 8600,

        /// <summary>
        /// The remote create cross reference operation failed on the Domain Naming Master FSMO.  The operation's error is in the extended data.
        /// </summary>
        DS_REMOTE_CROSSREF_OP_FAILED = 8601,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_CROSS_REF_BUSY = 8602,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_CANT_DERIVE_SPN_FOR_DELETED_DOMAIN = 8603,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_CANT_DEMOTE_WITH_WRITEABLE_NC = 8604,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_DUPLICATE_ID_FOUND = 8605,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_INSUFFICIENT_ATTR_TO_CREATE_OBJECT = 8606,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_GROUP_CONVERSION_ERROR = 8607,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_CANT_MOVE_APP_BASIC_GROUP = 8608,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_CANT_MOVE_APP_QUERY_GROUP = 8609,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_ROLE_NOT_VERIFIED = 8610,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_WKO_CONTAINER_CANNOT_BE_SPECIAL = 8611,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_DOMAIN_RENAME_IN_PROGRESS = 8612,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_EXISTING_AD_CHILD_NC = 8613,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_REPL_LIFETIME_EXCEEDED = 8614,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_DISALLOWED_IN_SYSTEM_CONTAINER = 8615,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DS_LDAP_SEND_QUEUE_FULL = 8616,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_RESPONSE_CODES_BASE = 9000,

        /// <summary>
        /// DNS server unable to interpret format.
        /// </summary>
        DNS_ERROR_RCODE_FORMAT_ERROR = 9001,

        /// <summary>
        /// DNS server failure.
        /// </summary>
        DNS_ERROR_RCODE_SERVER_FAILURE = 9002,

        /// <summary>
        /// DNS name does not exist.
        /// </summary>
        DNS_ERROR_RCODE_NAME_ERROR = 9003,

        /// <summary>
        /// DNS request not supported by name server.
        /// </summary>
        DNS_ERROR_RCODE_NOT_IMPLEMENTED = 9004,

        /// <summary>
        /// DNS operation refused.
        /// </summary>
        DNS_ERROR_RCODE_REFUSED = 9005,

        /// <summary>
        /// DNS name that ought not exist, does exist.
        /// </summary>
        DNS_ERROR_RCODE_YXDOMAIN = 9006,

        /// <summary>
        /// DNS RR set that ought not exist, does exist.
        /// </summary>
        DNS_ERROR_RCODE_YXRRSET = 9007,

        /// <summary>
        /// DNS RR set that ought to exist, does not exist.
        /// </summary>
        DNS_ERROR_RCODE_NXRRSET = 9008,

        /// <summary>
        /// DNS server not authoritative for zone.
        /// </summary>
        DNS_ERROR_RCODE_NOTAUTH = 9009,

        /// <summary>
        /// DNS name in update or prereq is not in zone.
        /// </summary>
        DNS_ERROR_RCODE_NOTZONE = 9010,

        /// <summary>
        /// DNS signature failed to verify.
        /// </summary>
        DNS_ERROR_RCODE_BADSIG = 9016,

        /// <summary>
        /// DNS bad key.
        /// </summary>
        DNS_ERROR_RCODE_BADKEY = 9017,

        /// <summary>
        /// DNS signature validity expired.
        /// </summary>
        DNS_ERROR_RCODE_BADTIME = 9018,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_PACKET_FMT_BASE = 9500,

        /// <summary>
        /// No records found for given DNS query.
        /// </summary>
        DNS_INFO_NO_RECORDS = 9501,

        /// <summary>
        /// Bad DNS packet.
        /// </summary>
        DNS_ERROR_BAD_PACKET = 9502,

        /// <summary>
        /// No DNS packet.
        /// </summary>
        DNS_ERROR_NO_PACKET = 9503,

        /// <summary>
        /// DNS error, check rcode.
        /// </summary>
        DNS_ERROR_RCODE = 9504,

        /// <summary>
        /// Unsecured DNS packet.
        /// </summary>
        DNS_ERROR_UNSECURE_PACKET = 9505,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_GENERAL_API_BASE = 9550,

        /// <summary>
        /// Invalid DNS type.
        /// </summary>
        DNS_ERROR_INVALID_TYPE = 9551,

        /// <summary>
        /// Invalid IP address.
        /// </summary>
        DNS_ERROR_INVALID_IP_ADDRESS = 9552,

        /// <summary>
        /// Invalid property.
        /// </summary>
        DNS_ERROR_INVALID_PROPERTY = 9553,

        /// <summary>
        /// Try DNS operation again later.
        /// </summary>
        DNS_ERROR_TRY_AGAIN_LATER = 9554,

        /// <summary>
        /// Record for given name and type is not unique.
        /// </summary>
        DNS_ERROR_NOT_UNIQUE = 9555,

        /// <summary>
        /// DNS name does not comply with RFC specifications.
        /// </summary>
        DNS_ERROR_NON_RFC_NAME = 9556,

        /// <summary>
        /// DNS name is a fully-qualified DNS name.
        /// </summary>
        DNS_STATUS_FQDN = 9557,

        /// <summary>
        /// DNS name is dotted (multi-label).
        /// </summary>
        DNS_STATUS_DOTTED_NAME = 9558,

        /// <summary>
        /// DNS name is a single-part name.
        /// </summary>
        DNS_STATUS_SINGLE_PART_NAME = 9559,

        /// <summary>
        /// DNS name contains an invalid character.
        /// </summary>
        DNS_ERROR_INVALID_NAME_CHAR = 9560,

        /// <summary>
        /// DNS name is entirely numeric.
        /// </summary>
        DNS_ERROR_NUMERIC_NAME = 9561,

        /// <summary>
        /// The operation requested is not permitted on a DNS root server.
        /// </summary>
        DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER = 9562,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_NOT_ALLOWED_UNDER_DELEGATION = 9563,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_CANNOT_FIND_ROOT_HINTS = 9564,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_INCONSISTENT_ROOT_HINTS = 9565,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_ZONE_BASE = 9600,

        /// <summary>
        /// DNS zone does not exist.
        /// </summary>
        DNS_ERROR_ZONE_DOES_NOT_EXIST = 9601,

        /// <summary>
        /// DNS zone information not available.
        /// </summary>
        DNS_ERROR_NO_ZONE_INFO = 9602,

        /// <summary>
        /// Invalid operation for DNS zone.
        /// </summary>
        DNS_ERROR_INVALID_ZONE_OPERATION = 9603,

        /// <summary>
        /// Invalid DNS zone configuration.
        /// </summary>
        DNS_ERROR_ZONE_CONFIGURATION_ERROR = 9604,

        /// <summary>
        /// DNS zone has no start of authority (SOA) record.
        /// </summary>
        DNS_ERROR_ZONE_HAS_NO_SOA_RECORD = 9605,

        /// <summary>
        /// DNS zone has no Name Server (NS) record.
        /// </summary>
        DNS_ERROR_ZONE_HAS_NO_NS_RECORDS = 9606,

        /// <summary>
        /// DNS zone is locked.
        /// </summary>
        DNS_ERROR_ZONE_LOCKED = 9607,

        /// <summary>
        /// DNS zone creation failed.
        /// </summary>
        DNS_ERROR_ZONE_CREATION_FAILED = 9608,

        /// <summary>
        /// DNS zone already exists.
        /// </summary>
        DNS_ERROR_ZONE_ALREADY_EXISTS = 9609,

        /// <summary>
        /// DNS automatic zone already exists.
        /// </summary>
        DNS_ERROR_AUTOZONE_ALREADY_EXISTS = 9610,

        /// <summary>
        /// Invalid DNS zone type.
        /// </summary>
        DNS_ERROR_INVALID_ZONE_TYPE = 9611,

        /// <summary>
        /// Secondary DNS zone requires master IP address.
        /// </summary>
        DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP = 9612,

        /// <summary>
        /// DNS zone not secondary.
        /// </summary>
        DNS_ERROR_ZONE_NOT_SECONDARY = 9613,

        /// <summary>
        /// Need secondary IP address.
        /// </summary>
        DNS_ERROR_NEED_SECONDARY_ADDRESSES = 9614,

        /// <summary>
        /// WINS initialization failed.
        /// </summary>
        DNS_ERROR_WINS_INIT_FAILED = 9615,

        /// <summary>
        /// Need WINS servers.
        /// </summary>
        DNS_ERROR_NEED_WINS_SERVERS = 9616,

        /// <summary>
        /// NBTSTAT initialization call failed.
        /// </summary>
        DNS_ERROR_NBSTAT_INIT_FAILED = 9617,

        /// <summary>
        /// Invalid delete of start of authority (SOA)
        /// </summary>
        DNS_ERROR_SOA_DELETE_INVALID = 9618,

        /// <summary>
        /// A conditional forwarding zone already exists for that name.
        /// </summary>
        DNS_ERROR_FORWARDER_ALREADY_EXISTS = 9619,

        /// <summary>
        /// This zone must be configured with one or more master DNS server IP addresses.
        /// </summary>
        DNS_ERROR_ZONE_REQUIRES_MASTER_IP = 9620,

        /// <summary>
        /// The operation cannot be performed because this zone is shutdown.
        /// </summary>
        DNS_ERROR_ZONE_IS_SHUTDOWN = 9621,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_DATAFILE_BASE = 9650,

        /// <summary>
        /// Primary DNS zone requires datafile.
        /// </summary>
        DNS_ERROR_PRIMARY_REQUIRES_DATAFILE = 9651,

        /// <summary>
        /// Invalid datafile name for DNS zone.
        /// </summary>
        DNS_ERROR_INVALID_DATAFILE_NAME = 9652,

        /// <summary>
        /// Failed to open datafile for DNS zone.
        /// </summary>
        DNS_ERROR_DATAFILE_OPEN_FAILURE = 9653,

        /// <summary>
        /// Failed to write datafile for DNS zone.
        /// </summary>
        DNS_ERROR_FILE_WRITEBACK_FAILED = 9654,

        /// <summary>
        /// Failure while reading datafile for DNS zone.
        /// </summary>
        DNS_ERROR_DATAFILE_PARSING = 9655,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_DATABASE_BASE = 9700,

        /// <summary>
        /// DNS record does not exist.
        /// </summary>
        DNS_ERROR_RECORD_DOES_NOT_EXIST = 9701,

        /// <summary>
        /// DNS record format error.
        /// </summary>
        DNS_ERROR_RECORD_FORMAT = 9702,

        /// <summary>
        /// Node creation failure in DNS.
        /// </summary>
        DNS_ERROR_NODE_CREATION_FAILED = 9703,

        /// <summary>
        /// Unknown DNS record type.
        /// </summary>
        DNS_ERROR_UNKNOWN_RECORD_TYPE = 9704,

        /// <summary>
        /// DNS record timed out.
        /// </summary>
        DNS_ERROR_RECORD_TIMED_OUT = 9705,

        /// <summary>
        /// Name not in DNS zone.
        /// </summary>
        DNS_ERROR_NAME_NOT_IN_ZONE = 9706,

        /// <summary>
        /// CNAME loop detected.
        /// </summary>
        DNS_ERROR_CNAME_LOOP = 9707,

        /// <summary>
        /// Node is a CNAME DNS record.
        /// </summary>
        DNS_ERROR_NODE_IS_CNAME = 9708,

        /// <summary>
        /// A CNAME record already exists for given name.
        /// </summary>
        DNS_ERROR_CNAME_COLLISION = 9709,

        /// <summary>
        /// Record only at DNS zone root.
        /// </summary>
        DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT = 9710,

        /// <summary>
        /// DNS record already exists.
        /// </summary>
        DNS_ERROR_RECORD_ALREADY_EXISTS = 9711,

        /// <summary>
        /// Secondary DNS zone data error.
        /// </summary>
        DNS_ERROR_SECONDARY_DATA = 9712,

        /// <summary>
        /// Could not create DNS cache data.
        /// </summary>
        DNS_ERROR_NO_CREATE_CACHE_DATA = 9713,

        /// <summary>
        /// DNS name does not exist.
        /// </summary>
        DNS_ERROR_NAME_DOES_NOT_EXIST = 9714,

        /// <summary>
        /// Could not create pointer (PTR) record.
        /// </summary>
        DNS_WARNING_PTR_CREATE_FAILED = 9715,

        /// <summary>
        /// DNS domain was undeleted.
        /// </summary>
        DNS_WARNING_DOMAIN_UNDELETED = 9716,

        /// <summary>
        /// The directory service is unavailable.
        /// </summary>
        DNS_ERROR_DS_UNAVAILABLE = 9717,

        /// <summary>
        /// DNS zone already exists in the directory service.
        /// </summary>
        DNS_ERROR_DS_ZONE_ALREADY_EXISTS = 9718,

        /// <summary>
        /// DNS server not creating or reading the boot file for the directory service integrated DNS zone.
        /// </summary>
        DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE = 9719,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_OPERATION_BASE = 9750,

        /// <summary>
        /// DNS AXFR (zone transfer) complete.
        /// </summary>
        DNS_INFO_AXFR_COMPLETE = 9751,

        /// <summary>
        /// DNS zone transfer failed.
        /// </summary>
        DNS_ERROR_AXFR = 9752,

        /// <summary>
        /// Added local WINS server.
        /// </summary>
        DNS_INFO_ADDED_LOCAL_WINS = 9753,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_SECURE_BASE = 9800,

        /// <summary>
        /// Secure update call needs to continue update request.
        /// </summary>
        DNS_STATUS_CONTINUE_NEEDED = 9801,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_SETUP_BASE = 9850,

        /// <summary>
        /// TCP/IP network protocol not installed.
        /// </summary>
        DNS_ERROR_NO_TCPIP = 9851,

        /// <summary>
        /// No DNS servers configured for local system.
        /// </summary>
        DNS_ERROR_NO_DNS_SERVERS = 9852,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_DP_BASE = 9900,

        /// <summary>
        /// The specified directory partition does not exist.
        /// </summary>
        DNS_ERROR_DP_DOES_NOT_EXIST = 9901,

        /// <summary>
        /// The specified directory partition already exists.
        /// </summary>
        DNS_ERROR_DP_ALREADY_EXISTS = 9902,

        /// <summary>
        /// The DS is not enlisted in the specified directory partition.
        /// </summary>
        DNS_ERROR_DP_NOT_ENLISTED = 9903,

        /// <summary>
        /// The DS is already enlisted in the specified directory partition.
        /// </summary>
        DNS_ERROR_DP_ALREADY_ENLISTED = 9904,

        /// <summary>
        /// No information avialable.
        /// </summary>
        DNS_ERROR_DP_NOT_AVAILABLE = 9905,

        /// <summary>
        /// No information avialable.
        /// </summary>
        WSABASEERR = 10000,

        /// <summary>
        /// A blocking operation was interrupted by a call to WSACancelBlockingCall.
        /// </summary>
        WSAEINTR = 10004,

        /// <summary>
        /// The file handle supplied is not valid.
        /// </summary>
        WSAEBADF = 10009,

        /// <summary>
        /// An attempt was made to access a socket in a way forbidden by its access permissions.
        /// </summary>
        WSAEACCES = 10013,

        /// <summary>
        /// The system detected an invalid pointer address in attempting to use a pointer argument in a call.
        /// </summary>
        WSAEFAULT = 10014,

        /// <summary>
        /// An invalid argument was supplied.
        /// </summary>
        WSAEINVAL = 10022,

        /// <summary>
        /// Too many open sockets.
        /// </summary>
        WSAEMFILE = 10024,

        /// <summary>
        /// A non-blocking socket operation could not be completed immediately.
        /// </summary>
        WSAEWOULDBLOCK = 10035,

        /// <summary>
        /// A blocking operation is currently executing.
        /// </summary>
        WSAEINPROGRESS = 10036,

        /// <summary>
        /// An operation was attempted on a non-blocking socket that already had an operation in progress.
        /// </summary>
        WSAEALREADY = 10037,

        /// <summary>
        /// An operation was attempted on something that is not a socket.
        /// </summary>
        WSAENOTSOCK = 10038,

        /// <summary>
        /// A required address was omitted from an operation on a socket.
        /// </summary>
        WSAEDESTADDRREQ = 10039,

        /// <summary>
        /// A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself.
        /// </summary>
        WSAEMSGSIZE = 10040,

        /// <summary>
        /// A protocol was specified in the socket function call that does not support the semantics of the socket type requested.
        /// </summary>
        WSAEPROTOTYPE = 10041,

        /// <summary>
        /// An unknown, invalid, or unsupported option or level was specified in a getsockopt or setsockopt call.
        /// </summary>
        WSAENOPROTOOPT = 10042,

        /// <summary>
        /// The requested protocol has not been configured into the system, or no implementation for it exists.
        /// </summary>
        WSAEPROTONOSUPPORT = 10043,

        /// <summary>
        /// The support for the specified socket type does not exist in this address family.
        /// </summary>
        WSAESOCKTNOSUPPORT = 10044,

        /// <summary>
        /// The attempted operation is not supported for the type of object referenced.
        /// </summary>
        WSAEOPNOTSUPP = 10045,

        /// <summary>
        /// The protocol family has not been configured into the system or no implementation for it exists.
        /// </summary>
        WSAEPFNOSUPPORT = 10046,

        /// <summary>
        /// An address incompatible with the requested protocol was used.
        /// </summary>
        WSAEAFNOSUPPORT = 10047,

        /// <summary>
        /// Only one usage of each socket address (protocol/network address/port) is normally permitted.
        /// </summary>
        WSAEADDRINUSE = 10048,

        /// <summary>
        /// The requested address is not valid in its context.
        /// </summary>
        WSAEADDRNOTAVAIL = 10049,

        /// <summary>
        /// A socket operation encountered a dead network.
        /// </summary>
        WSAENETDOWN = 10050,

        /// <summary>
        /// A socket operation was attempted to an unreachable network.
        /// </summary>
        WSAENETUNREACH = 10051,

        /// <summary>
        /// The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress.
        /// </summary>
        WSAENETRESET = 10052,

        /// <summary>
        /// An established connection was aborted by the software in your host machine.
        /// </summary>
        WSAECONNABORTED = 10053,

        /// <summary>
        /// An existing connection was forcibly closed by the remote host.
        /// </summary>
        WSAECONNRESET = 10054,

        /// <summary>
        /// An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.
        /// </summary>
        WSAENOBUFS = 10055,

        /// <summary>
        /// A connect request was made on an already connected socket.
        /// </summary>
        WSAEISCONN = 10056,

        /// <summary>
        /// A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied.
        /// </summary>
        WSAENOTCONN = 10057,

        /// <summary>
        /// A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call.
        /// </summary>
        WSAESHUTDOWN = 10058,

        /// <summary>
        /// Too many references to some kernel object.
        /// </summary>
        WSAETOOMANYREFS = 10059,

        /// <summary>
        /// A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.
        /// </summary>
        WSAETIMEDOUT = 10060,

        /// <summary>
        /// No connection could be made because the target machine actively refused it.
        /// </summary>
        WSAECONNREFUSED = 10061,

        /// <summary>
        /// Cannot translate name.
        /// </summary>
        WSAELOOP = 10062,

        /// <summary>
        /// Name component or name was too long.
        /// </summary>
        WSAENAMETOOLONG = 10063,

        /// <summary>
        /// A socket operation failed because the destination host was down.
        /// </summary>
        WSAEHOSTDOWN = 10064,

        /// <summary>
        /// A socket operation was attempted to an unreachable host.
        /// </summary>
        WSAEHOSTUNREACH = 10065,

        /// <summary>
        /// Cannot remove a directory that is not empty.
        /// </summary>
        WSAENOTEMPTY = 10066,

        /// <summary>
        /// A Windows Sockets implementation may have a limit on the number of applications that may use it simultaneously.
        /// </summary>
        WSAEPROCLIM = 10067,

        /// <summary>
        /// Ran out of quota.
        /// </summary>
        WSAEUSERS = 10068,

        /// <summary>
        /// Ran out of disk quota.
        /// </summary>
        WSAEDQUOT = 10069,

        /// <summary>
        /// File handle reference is no longer available.
        /// </summary>
        WSAESTALE = 10070,

        /// <summary>
        /// Item is not available locally.
        /// </summary>
        WSAEREMOTE = 10071,

        /// <summary>
        /// WSAStartup cannot function at this time because the underlying system it uses to provide network services is currently unavailable.
        /// </summary>
        WSASYSNOTREADY = 10091,

        /// <summary>
        /// The Windows Sockets version requested is not supported.
        /// </summary>
        WSAVERNOTSUPPORTED = 10092,

        /// <summary>
        /// Either the application has not called WSAStartup, or WSAStartup failed.
        /// </summary>
        WSANOTINITIALISED = 10093,

        /// <summary>
        /// Returned by WSARecv or WSARecvFrom to indicate the remote party has initiated a graceful shutdown sequence.
        /// </summary>
        WSAEDISCON = 10101,

        /// <summary>
        /// No more results can be returned by WSALookupServiceNext.
        /// </summary>
        WSAENOMORE = 10102,

        /// <summary>
        /// A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.
        /// </summary>
        WSAECANCELLED = 10103,

        /// <summary>
        /// The procedure call table is invalid.
        /// </summary>
        WSAEINVALIDPROCTABLE = 10104,

        /// <summary>
        /// The requested service provider is invalid.
        /// </summary>
        WSAEINVALIDPROVIDER = 10105,

        /// <summary>
        /// The requested service provider could not be loaded or initialized.
        /// </summary>
        WSAEPROVIDERFAILEDINIT = 10106,

        /// <summary>
        /// A system call that should never fail has failed.
        /// </summary>
        WSASYSCALLFAILURE = 10107,

        /// <summary>
        /// No such service is known. The service cannot be found in the specified name space.
        /// </summary>
        WSASERVICE_NOT_FOUND = 10108,

        /// <summary>
        /// The specified class was not found.
        /// </summary>
        WSATYPE_NOT_FOUND = 10109,

        /// <summary>
        /// No more results can be returned by WSALookupServiceNext.
        /// </summary>
        WSA_E_NO_MORE = 10110,

        /// <summary>
        /// A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.
        /// </summary>
        WSA_E_CANCELLED = 10111,

        /// <summary>
        /// A database query failed because it was actively refused.
        /// </summary>
        WSAEREFUSED = 10112,

        /// <summary>
        /// No such host is known.
        /// </summary>
        WSAHOST_NOT_FOUND = 11001,

        /// <summary>
        /// This is usually a temporary error during hostname resolution and means that the local server did not receive a response from an authoritative server.
        /// </summary>
        WSATRY_AGAIN = 11002,

        /// <summary>
        /// A non-recoverable error occurred during a database lookup.
        /// </summary>
        WSANO_RECOVERY = 11003,

        /// <summary>
        /// The requested name is valid and was found in the database, but it does not have the correct associated data being resolved for.
        /// </summary>
        WSANO_DATA = 11004,

        /// <summary>
        /// At least one reserve has arrived.
        /// </summary>
        WSA_QOS_RECEIVERS = 11005,

        /// <summary>
        /// At least one path has arrived.
        /// </summary>
        WSA_QOS_SENDERS = 11006,

        /// <summary>
        /// There are no senders.
        /// </summary>
        WSA_QOS_NO_SENDERS = 11007,

        /// <summary>
        /// There are no receivers.
        /// </summary>
        WSA_QOS_NO_RECEIVERS = 11008,

        /// <summary>
        /// Reserve has been confirmed.
        /// </summary>
        WSA_QOS_REQUEST_CONFIRMED = 11009,

        /// <summary>
        /// Error due to lack of resources.
        /// </summary>
        WSA_QOS_ADMISSION_FAILURE = 11010,

        /// <summary>
        /// Rejected for administrative reasons - bad credentials.
        /// </summary>
        WSA_QOS_POLICY_FAILURE = 11011,

        /// <summary>
        /// Unknown or conflicting style.
        /// </summary>
        WSA_QOS_BAD_STYLE = 11012,

        /// <summary>
        /// Problem with some part of the filterspec or providerspecific buffer in general.
        /// </summary>
        WSA_QOS_BAD_OBJECT = 11013,

        /// <summary>
        /// Problem with some part of the flowspec.
        /// </summary>
        WSA_QOS_TRAFFIC_CTRL_ERROR = 11014,

        /// <summary>
        /// General QOS error.
        /// </summary>
        WSA_QOS_GENERIC_ERROR = 11015,

        /// <summary>
        /// An invalid or unrecognized service type was found in the flowspec.
        /// </summary>
        WSA_QOS_ESERVICETYPE = 11016,

        /// <summary>
        /// An invalid or inconsistent flowspec was found in the QOS structure.
        /// </summary>
        WSA_QOS_EFLOWSPEC = 11017,

        /// <summary>
        /// Invalid QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_EPROVSPECBUF = 11018,

        /// <summary>
        /// An invalid QOS filter style was used.
        /// </summary>
        WSA_QOS_EFILTERSTYLE = 11019,

        /// <summary>
        /// An invalid QOS filter type was used.
        /// </summary>
        WSA_QOS_EFILTERTYPE = 11020,

        /// <summary>
        /// An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.
        /// </summary>
        WSA_QOS_EFILTERCOUNT = 11021,

        /// <summary>
        /// An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_EOBJLENGTH = 11022,

        /// <summary>
        /// An incorrect number of flow descriptors was specified in the QOS structure.
        /// </summary>
        WSA_QOS_EFLOWCOUNT = 11023,

        /// <summary>
        /// An unrecognized object was found in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_EUNKOWNPSOBJ = 11024,

        /// <summary>
        /// An invalid policy object was found in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_EPOLICYOBJ = 11025,

        /// <summary>
        /// An invalid QOS flow descriptor was found in the flow descriptor list.
        /// </summary>
        WSA_QOS_EFLOWDESC = 11026,

        /// <summary>
        /// An invalid or inconsistent flowspec was found in the QOS provider specific buffer.
        /// </summary>
        WSA_QOS_EPSFLOWSPEC = 11027,

        /// <summary>
        /// An invalid FILTERSPEC was found in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_EPSFILTERSPEC = 11028,

        /// <summary>
        /// An invalid shape discard mode object was found in the QOS provider specific buffer.
        /// </summary>
        WSA_QOS_ESDMODEOBJ = 11029,

        /// <summary>
        /// An invalid shaping rate object was found in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_ESHAPERATEOBJ = 11030,

        /// <summary>
        /// A reserved policy element was found in the QOS provider-specific buffer.
        /// </summary>
        WSA_QOS_RESERVED_PETYPE = 11031,

        /// <summary>
        /// The requested section was not present in the activation context.
        /// </summary>
        SXS_SECTION_NOT_FOUND = 14000,

        /// <summary>
        /// This application has failed to start because the application configuration is incorrect. Reinstalling the application may fix this problem.
        /// </summary>
        SXS_CANT_GEN_ACTCTX = 14001,

        /// <summary>
        /// The application binding data format is invalid.
        /// </summary>
        SXS_INVALID_ACTCTXDATA_FORMAT = 14002,

        /// <summary>
        /// The referenced assembly is not installed on your system.
        /// </summary>
        SXS_ASSEMBLY_NOT_FOUND = 14003,

        /// <summary>
        /// The manifest file does not begin with the required tag and format information.
        /// </summary>
        SXS_MANIFEST_FORMAT_ERROR = 14004,

        /// <summary>
        /// The manifest file contains one or more syntax errors.
        /// </summary>
        SXS_MANIFEST_PARSE_ERROR = 14005,

        /// <summary>
        /// The application attempted to activate a disabled activation context.
        /// </summary>
        SXS_ACTIVATION_CONTEXT_DISABLED = 14006,

        /// <summary>
        /// The requested lookup key was not found in any active activation context.
        /// </summary>
        SXS_KEY_NOT_FOUND = 14007,

        /// <summary>
        /// A component version required by the application conflicts with another component version already active.
        /// </summary>
        SXS_VERSION_CONFLICT = 14008,

        /// <summary>
        /// The type requested activation context section does not match the query API used.
        /// </summary>
        SXS_WRONG_SECTION_TYPE = 14009,

        /// <summary>
        /// Lack of system resources has required isolated activation to be disabled for the current thread of execution.
        /// </summary>
        SXS_THREAD_QUERIES_DISABLED = 14010,

        /// <summary>
        /// An attempt to set the process default activation context failed because the process default activation context was already set.
        /// </summary>
        SXS_PROCESS_DEFAULT_ALREADY_SET = 14011,

        /// <summary>
        /// The encoding group identifier specified is not recognized.
        /// </summary>
        SXS_UNKNOWN_ENCODING_GROUP = 14012,

        /// <summary>
        /// The encoding requested is not recognized.
        /// </summary>
        SXS_UNKNOWN_ENCODING = 14013,

        /// <summary>
        /// The manifest contains a reference to an invalid URI.
        /// </summary>
        SXS_INVALID_XML_NAMESPACE_URI = 14014,

        /// <summary>
        /// The application manifest contains a reference to a dependent assembly which is not installed
        /// </summary>
        SXS_ROOT_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14015,

        /// <summary>
        /// The manifest for an assembly used by the application has a reference to a dependent assembly which is not installed
        /// </summary>
        SXS_LEAF_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14016,

        /// <summary>
        /// The manifest contains an attribute for the assembly identity which is not valid.
        /// </summary>
        SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE = 14017,

        /// <summary>
        /// The manifest is missing the required default namespace specification on the assembly element.
        /// </summary>
        SXS_MANIFEST_MISSING_REQUIRED_DEFAULT_NAMESPACE = 14018,

        /// <summary>
        /// The manifest has a default namespace specified on the assembly element but its value is not "urn:schemas-microsoft-com:asm.v1".
        /// </summary>
        SXS_MANIFEST_INVALID_REQUIRED_DEFAULT_NAMESPACE = 14019,

        /// <summary>
        /// The private manifest probed has crossed reparse-point-associated path
        /// </summary>
        SXS_PRIVATE_MANIFEST_CROSS_PATH_WITH_REPARSE_POINT = 14020,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have files by the same name.
        /// </summary>
        SXS_DUPLICATE_DLL_NAME = 14021,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have window classes with the same name.
        /// </summary>
        SXS_DUPLICATE_WINDOWCLASS_NAME = 14022,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have the same COM server CLSIDs.
        /// </summary>
        SXS_DUPLICATE_CLSID = 14023,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have proxies for the same COM interface IIDs.
        /// </summary>
        SXS_DUPLICATE_IID = 14024,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have the same COM type library TLBIDs.
        /// </summary>
        SXS_DUPLICATE_TLBID = 14025,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest have the same COM ProgIDs.
        /// </summary>
        SXS_DUPLICATE_PROGID = 14026,

        /// <summary>
        /// Two or more components referenced directly or indirectly by the application manifest are different versions of the same component which is not permitted.
        /// </summary>
        SXS_DUPLICATE_ASSEMBLY_NAME = 14027,

        /// <summary>
        /// A component's file does not match the verification information present in the
        /// component manifest.
        /// </summary>
        SXS_FILE_HASH_MISMATCH = 14028,

        /// <summary>
        /// The policy manifest contains one or more syntax errors.
        /// </summary>
        SXS_POLICY_PARSE_ERROR = 14029,

        /// <summary>
        /// Manifest Parse Error : A string literal was expected, but no opening quote character was found.
        /// </summary>
        SXS_XML_E_MISSINGQUOTE = 14030,

        /// <summary>
        /// Manifest Parse Error : Incorrect syntax was used in a comment.
        /// </summary>
        SXS_XML_E_COMMENTSYNTAX = 14031,

        /// <summary>
        /// Manifest Parse Error : A name was started with an invalid character.
        /// </summary>
        SXS_XML_E_BADSTARTNAMECHAR = 14032,

        /// <summary>
        /// Manifest Parse Error : A name contained an invalid character.
        /// </summary>
        SXS_XML_E_BADNAMECHAR = 14033,

        /// <summary>
        /// Manifest Parse Error : A string literal contained an invalid character.
        /// </summary>
        SXS_XML_E_BADCHARINSTRING = 14034,

        /// <summary>
        /// Manifest Parse Error : Invalid syntax for an xml declaration.
        /// </summary>
        SXS_XML_E_XMLDECLSYNTAX = 14035,

        /// <summary>
        /// Manifest Parse Error : An Invalid character was found in text content.
        /// </summary>
        SXS_XML_E_BADCHARDATA = 14036,

        /// <summary>
        /// Manifest Parse Error : Required white space was missing.
        /// </summary>
        SXS_XML_E_MISSINGWHITESPACE = 14037,

        /// <summary>
        /// Manifest Parse Error : The character '>' was expected.
        /// </summary>
        SXS_XML_E_EXPECTINGTAGEND = 14038,

        /// <summary>
        /// Manifest Parse Error : A semi colon character was expected.
        /// </summary>
        SXS_XML_E_MISSINGSEMICOLON = 14039,

        /// <summary>
        /// Manifest Parse Error : Unbalanced parentheses.
        /// </summary>
        SXS_XML_E_UNBALANCEDPAREN = 14040,

        /// <summary>
        /// Manifest Parse Error : Internal error.
        /// </summary>
        SXS_XML_E_INTERNALERROR = 14041,

        /// <summary>
        /// Manifest Parse Error : Whitespace is not allowed at this location.
        /// </summary>
        SXS_XML_E_UNEXPECTED_WHITESPACE = 14042,

        /// <summary>
        /// Manifest Parse Error : End of file reached in invalid state for current encoding.
        /// </summary>
        SXS_XML_E_INCOMPLETE_ENCODING = 14043,

        /// <summary>
        /// Manifest Parse Error : Missing parenthesis.
        /// </summary>
        SXS_XML_E_MISSING_PAREN = 14044,

        /// <summary>
        /// Manifest Parse Error : A single or double closing quote character (\' or \") is missing.
        /// </summary>
        SXS_XML_E_EXPECTINGCLOSEQUOTE = 14045,

        /// <summary>
        /// Manifest Parse Error : Multiple colons are not allowed in a name.
        /// </summary>
        SXS_XML_E_MULTIPLE_COLONS = 14046,

        /// <summary>
        /// Manifest Parse Error : Invalid character for decimal digit.
        /// </summary>
        SXS_XML_E_INVALID_DECIMAL = 14047,

        /// <summary>
        /// Manifest Parse Error : Invalid character for hexidecimal digit.
        /// </summary>
        SXS_XML_E_INVALID_HEXIDECIMAL = 14048,

        /// <summary>
        /// Manifest Parse Error : Invalid unicode character value for this platform.
        /// </summary>
        SXS_XML_E_INVALID_UNICODE = 14049,

        /// <summary>
        /// Manifest Parse Error : Expecting whitespace or '?'.
        /// </summary>
        SXS_XML_E_WHITESPACEORQUESTIONMARK = 14050,

        /// <summary>
        /// Manifest Parse Error : End tag was not expected at this location.
        /// </summary>
        SXS_XML_E_UNEXPECTEDENDTAG = 14051,

        /// <summary>
        /// Manifest Parse Error : The following tags were not closed: %1.
        /// </summary>
        SXS_XML_E_UNCLOSEDTAG = 14052,

        /// <summary>
        /// Manifest Parse Error : Duplicate attribute.
        /// </summary>
        SXS_XML_E_DUPLICATEATTRIBUTE = 14053,

        /// <summary>
        /// Manifest Parse Error : Only one top level element is allowed in an XML document.
        /// </summary>
        SXS_XML_E_MULTIPLEROOTS = 14054,

        /// <summary>
        /// Manifest Parse Error : Invalid at the top level of the document.
        /// </summary>
        SXS_XML_E_INVALIDATROOTLEVEL = 14055,

        /// <summary>
        /// Manifest Parse Error : Invalid xml declaration.
        /// </summary>
        SXS_XML_E_BADXMLDECL = 14056,

        /// <summary>
        /// Manifest Parse Error : XML document must have a top level element.
        /// </summary>
        SXS_XML_E_MISSINGROOT = 14057,

        /// <summary>
        /// Manifest Parse Error : Unexpected end of file.
        /// </summary>
        SXS_XML_E_UNEXPECTEDEOF = 14058,

        /// <summary>
        /// Manifest Parse Error : Parameter entities cannot be used inside markup declarations in an internal subset.
        /// </summary>
        SXS_XML_E_BADPEREFINSUBSET = 14059,

        /// <summary>
        /// Manifest Parse Error : Element was not closed.
        /// </summary>
        SXS_XML_E_UNCLOSEDSTARTTAG = 14060,

        /// <summary>
        /// Manifest Parse Error : End element was missing the character '>'.
        /// </summary>
        SXS_XML_E_UNCLOSEDENDTAG = 14061,

        /// <summary>
        /// Manifest Parse Error : A string literal was not closed.
        /// </summary>
        SXS_XML_E_UNCLOSEDSTRING = 14062,

        /// <summary>
        /// Manifest Parse Error : A comment was not closed.
        /// </summary>
        SXS_XML_E_UNCLOSEDCOMMENT = 14063,

        /// <summary>
        /// Manifest Parse Error : A declaration was not closed.
        /// </summary>
        SXS_XML_E_UNCLOSEDDECL = 14064,

        /// <summary>
        /// Manifest Parse Error : A CDATA section was not closed.
        /// </summary>
        SXS_XML_E_UNCLOSEDCDATA = 14065,

        /// <summary>
        /// Manifest Parse Error : The namespace prefix is not allowed to start with the reserved string "xml".
        /// </summary>
        SXS_XML_E_RESERVEDNAMESPACE = 14066,

        /// <summary>
        /// Manifest Parse Error : System does not support the specified encoding.
        /// </summary>
        SXS_XML_E_INVALIDENCODING = 14067,

        /// <summary>
        /// Manifest Parse Error : Switch from current encoding to specified encoding not supported.
        /// </summary>
        SXS_XML_E_INVALIDSWITCH = 14068,

        /// <summary>
        /// Manifest Parse Error : The name 'xml' is reserved and must be lower case.
        /// </summary>
        SXS_XML_E_BADXMLCASE = 14069,

        /// <summary>
        /// Manifest Parse Error : The standalone attribute must have the value 'yes' or 'no'.
        /// </summary>
        SXS_XML_E_INVALID_STANDALONE = 14070,

        /// <summary>
        /// Manifest Parse Error : The standalone attribute cannot be used in external entities.
        /// </summary>
        SXS_XML_E_UNEXPECTED_STANDALONE = 14071,

        /// <summary>
        /// Manifest Parse Error : Invalid version number.
        /// </summary>
        SXS_XML_E_INVALID_VERSION = 14072,

        /// <summary>
        /// Manifest Parse Error : Missing equals sign between attribute and attribute value.
        /// </summary>
        SXS_XML_E_MISSINGEQUALS = 14073,

        /// <summary>
        /// Assembly Protection Error : Unable to recover the specified assembly.
        /// </summary>
        SXS_PROTECTION_RECOVERY_FAILED = 14074,

        /// <summary>
        /// Assembly Protection Error : The public key for an assembly was too short to be allowed.
        /// </summary>
        SXS_PROTECTION_PUBLIC_KEY_TOO_LONG = 14075,

        /// <summary>
        /// Assembly Protection Error : The catalog for an assembly is not valid, or does not match the assembly's manifest.
        /// </summary>
        SXS_PROTECTION_CATALOG_NOT_VALID = 14076,

        /// <summary>
        /// An HRESULT could not be translated to a corresponding Win32 error code.
        /// </summary>
        SXS_UNTRANSLATABLE_HRESULT = 14077,

        /// <summary>
        /// Assembly Protection Error : The catalog for an assembly is missing.
        /// </summary>
        SXS_PROTECTION_CATALOG_FILE_MISSING = 14078,

        /// <summary>
        /// The supplied assembly identity is missing one or more attributes which must be present in this context.
        /// </summary>
        SXS_MISSING_ASSEMBLY_IDENTITY_ATTRIBUTE = 14079,

        /// <summary>
        /// The supplied assembly identity has one or more attribute names that contain characters not permitted in XML names.
        /// </summary>
        SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE_NAME = 14080,

        /// <summary>
        /// The specified quick mode policy already exists.
        /// </summary>
        IPSEC_QM_POLICY_EXISTS = 13000,

        /// <summary>
        /// The specified quick mode policy was not found.
        /// </summary>
        IPSEC_QM_POLICY_NOT_FOUND = 13001,

        /// <summary>
        /// The specified quick mode policy is being used.
        /// </summary>
        IPSEC_QM_POLICY_IN_USE = 13002,

        /// <summary>
        /// The specified main mode policy already exists.
        /// </summary>
        IPSEC_MM_POLICY_EXISTS = 13003,

        /// <summary>
        /// The specified main mode policy was not found
        /// </summary>
        IPSEC_MM_POLICY_NOT_FOUND = 13004,

        /// <summary>
        /// The specified main mode policy is being used.
        /// </summary>
        IPSEC_MM_POLICY_IN_USE = 13005,

        /// <summary>
        /// The specified main mode filter already exists.
        /// </summary>
        IPSEC_MM_FILTER_EXISTS = 13006,

        /// <summary>
        /// The specified main mode filter was not found.
        /// </summary>
        IPSEC_MM_FILTER_NOT_FOUND = 13007,

        /// <summary>
        /// The specified transport mode filter already exists.
        /// </summary>
        IPSEC_TRANSPORT_FILTER_EXISTS = 13008,

        /// <summary>
        /// The specified transport mode filter does not exist.
        /// </summary>
        IPSEC_TRANSPORT_FILTER_NOT_FOUND = 13009,

        /// <summary>
        /// The specified main mode authentication list exists.
        /// </summary>
        IPSEC_MM_AUTH_EXISTS = 13010,

        /// <summary>
        /// The specified main mode authentication list was not found.
        /// </summary>
        IPSEC_MM_AUTH_NOT_FOUND = 13011,

        /// <summary>
        /// The specified quick mode policy is being used.
        /// </summary>
        IPSEC_MM_AUTH_IN_USE = 13012,

        /// <summary>
        /// The specified main mode policy was not found.
        /// </summary>
        IPSEC_DEFAULT_MM_POLICY_NOT_FOUND = 13013,

        /// <summary>
        /// The specified quick mode policy was not found
        /// </summary>
        IPSEC_DEFAULT_MM_AUTH_NOT_FOUND = 13014,

        /// <summary>
        /// The manifest file contains one or more syntax errors.
        /// </summary>
        IPSEC_DEFAULT_QM_POLICY_NOT_FOUND = 13015,

        /// <summary>
        /// The application attempted to activate a disabled activation context.
        /// </summary>
        IPSEC_TUNNEL_FILTER_EXISTS = 13016,

        /// <summary>
        /// The requested lookup key was not found in any active activation context.
        /// </summary>
        IPSEC_TUNNEL_FILTER_NOT_FOUND = 13017,

        /// <summary>
        /// The Main Mode filter is pending deletion.
        /// </summary>
        IPSEC_MM_FILTER_PENDING_DELETION = 13018,

        /// <summary>
        /// The transport filter is pending deletion.
        /// </summary>
        IPSEC_TRANSPORT_FILTER_PENDING_DELETION = 13019,

        /// <summary>
        /// The tunnel filter is pending deletion.
        /// </summary>
        IPSEC_TUNNEL_FILTER_PENDING_DELETION = 13020,

        /// <summary>
        /// The Main Mode policy is pending deletion.
        /// </summary>
        IPSEC_MM_POLICY_PENDING_DELETION = 13021,

        /// <summary>
        /// The Main Mode authentication bundle is pending deletion.
        /// </summary>
        IPSEC_MM_AUTH_PENDING_DELETION = 13022,

        /// <summary>
        /// The Quick Mode policy is pending deletion.
        /// </summary>
        IPSEC_QM_POLICY_PENDING_DELETION = 13023,

        /// <summary>
        /// No information avialable.
        /// </summary>
        WARNING_IPSEC_MM_POLICY_PRUNED = 13024,

        /// <summary>
        /// No information avialable.
        /// </summary>
        WARNING_IPSEC_QM_POLICY_PRUNED = 13025,

        /// <summary>
        /// ERROR_IPSEC_IKE_NEG_STATUS_BEGIN
        /// </summary>
        IPSEC_IKE_NEG_STATUS_BEGIN = 13800,

        /// <summary>
        /// IKE authentication credentials are unacceptable
        /// </summary>
        IPSEC_IKE_AUTH_FAIL = 13801,

        /// <summary>
        /// IKE security attributes are unacceptable
        /// </summary>
        IPSEC_IKE_ATTRIB_FAIL = 13802,

        /// <summary>
        /// IKE Negotiation in progress
        /// </summary>
        IPSEC_IKE_NEGOTIATION_PENDING = 13803,

        /// <summary>
        /// General processing error
        /// </summary>
        IPSEC_IKE_GENERAL_PROCESSING_ERROR = 13804,

        /// <summary>
        /// Negotiation timed out
        /// </summary>
        IPSEC_IKE_TIMED_OUT = 13805,

        /// <summary>
        /// IKE failed to find valid machine certificate
        /// </summary>
        IPSEC_IKE_NO_CERT = 13806,

        /// <summary>
        /// IKE SA deleted by peer before establishment completed
        /// </summary>
        IPSEC_IKE_SA_DELETED = 13807,

        /// <summary>
        /// IKE SA deleted before establishment completed
        /// </summary>
        IPSEC_IKE_SA_REAPED = 13808,

        /// <summary>
        /// Negotiation request sat in Queue too long
        /// </summary>
        IPSEC_IKE_MM_ACQUIRE_DROP = 13809,

        /// <summary>
        /// Negotiation request sat in Queue too long
        /// </summary>
        IPSEC_IKE_QM_ACQUIRE_DROP = 13810,

        /// <summary>
        /// Negotiation request sat in Queue too long
        /// </summary>
        IPSEC_IKE_QUEUE_DROP_MM = 13811,

        /// <summary>
        /// Negotiation request sat in Queue too long
        /// </summary>
        IPSEC_IKE_QUEUE_DROP_NO_MM = 13812,

        /// <summary>
        /// No response from peer
        /// </summary>
        IPSEC_IKE_DROP_NO_RESPONSE = 13813,

        /// <summary>
        /// Negotiation took too long
        /// </summary>
        IPSEC_IKE_MM_DELAY_DROP = 13814,

        /// <summary>
        /// Negotiation took too long
        /// </summary>
        IPSEC_IKE_QM_DELAY_DROP = 13815,

        /// <summary>
        /// Unknown error occurred
        /// </summary>
        IPSEC_IKE_ERROR = 13816,

        /// <summary>
        /// Certificate Revocation Check failed
        /// </summary>
        IPSEC_IKE_CRL_FAILED = 13817,

        /// <summary>
        /// Invalid certificate key usage
        /// </summary>
        IPSEC_IKE_INVALID_KEY_USAGE = 13818,

        /// <summary>
        /// Invalid certificate type
        /// </summary>
        IPSEC_IKE_INVALID_CERT_TYPE = 13819,

        /// <summary>
        /// No private key associated with machine certificate
        /// </summary>
        IPSEC_IKE_NO_PRIVATE_KEY = 13820,

        /// <summary>
        /// Failure in Diffie-Helman computation
        /// </summary>
        IPSEC_IKE_DH_FAIL = 13822,

        /// <summary>
        /// Invalid header
        /// </summary>
        IPSEC_IKE_INVALID_HEADER = 13824,

        /// <summary>
        /// No policy configured
        /// </summary>
        IPSEC_IKE_NO_POLICY = 13825,

        /// <summary>
        /// Failed to verify signature
        /// </summary>
        IPSEC_IKE_INVALID_SIGNATURE = 13826,

        /// <summary>
        /// Failed to authenticate using kerberos
        /// </summary>
        IPSEC_IKE_KERBEROS_ERROR = 13827,

        /// <summary>
        /// Peer's certificate did not have a public key
        /// </summary>
        IPSEC_IKE_NO_PUBLIC_KEY = 13828,

        /// <summary>
        /// Error processing error payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR = 13829,

        /// <summary>
        /// Error processing SA payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_SA = 13830,

        /// <summary>
        /// Error processing Proposal payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_PROP = 13831,

        /// <summary>
        /// Error processing Transform payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_TRANS = 13832,

        /// <summary>
        /// Error processing KE payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_KE = 13833,

        /// <summary>
        /// Error processing ID payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_ID = 13834,

        /// <summary>
        /// Error processing Cert payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_CERT = 13835,

        /// <summary>
        /// Error processing Certificate Request payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_CERT_REQ = 13836,

        /// <summary>
        /// Error processing Hash payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_HASH = 13837,

        /// <summary>
        /// Error processing Signature payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_SIG = 13838,

        /// <summary>
        /// Error processing Nonce payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_NONCE = 13839,

        /// <summary>
        /// Error processing Notify payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_NOTIFY = 13840,

        /// <summary>
        /// Error processing Delete Payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_DELETE = 13841,

        /// <summary>
        /// Error processing VendorId payload
        /// </summary>
        IPSEC_IKE_PROCESS_ERR_VENDOR = 13842,

        /// <summary>
        /// Invalid payload received
        /// </summary>
        IPSEC_IKE_INVALID_PAYLOAD = 13843,

        /// <summary>
        /// Soft SA loaded
        /// </summary>
        IPSEC_IKE_LOAD_SOFT_SA = 13844,

        /// <summary>
        /// Soft SA torn down
        /// </summary>
        IPSEC_IKE_SOFT_SA_TORN_DOWN = 13845,

        /// <summary>
        /// Invalid cookie received.
        /// </summary>
        IPSEC_IKE_INVALID_COOKIE = 13846,

        /// <summary>
        /// Peer failed to send valid machine certificate
        /// </summary>
        IPSEC_IKE_NO_PEER_CERT = 13847,

        /// <summary>
        /// Certification Revocation check of peer's certificate failed
        /// </summary>
        IPSEC_IKE_PEER_CRL_FAILED = 13848,

        /// <summary>
        /// New policy invalidated SAs formed with old policy
        /// </summary>
        IPSEC_IKE_POLICY_CHANGE = 13849,

        /// <summary>
        /// There is no available Main Mode IKE policy.
        /// </summary>
        IPSEC_IKE_NO_MM_POLICY = 13850,

        /// <summary>
        /// Failed to enabled TCB privilege.
        /// </summary>
        IPSEC_IKE_NOTCBPRIV = 13851,

        /// <summary>
        /// Failed to load SECURITY.DLL.
        /// </summary>
        IPSEC_IKE_SECLOADFAIL = 13852,

        /// <summary>
        /// Failed to obtain security function table dispatch address from SSPI.
        /// </summary>
        IPSEC_IKE_FAILSSPINIT = 13853,

        /// <summary>
        /// Failed to query Kerberos package to obtain max token size.
        /// </summary>
        IPSEC_IKE_FAILQUERYSSP = 13854,

        /// <summary>
        /// Failed to obtain Kerberos server credentials for ISAKMP/ERROR_IPSEC_IKE service.  Kerberos authentication will not function.  The most likely reason for this is lack of domain membership.  This is normal if your computer is a member of a workgroup.
        /// </summary>
        IPSEC_IKE_SRVACQFAIL = 13855,

        /// <summary>
        /// Failed to determine SSPI principal name for ISAKMP/ERROR_IPSEC_IKE service (QueryCredentialsAttributes).
        /// </summary>
        IPSEC_IKE_SRVQUERYCRED = 13856,

        /// <summary>
        /// Failed to obtain new SPI for the inbound SA from Ipsec driver.  The most common cause for this is that the driver does not have the correct filter.  Check your policy to verify the filters.
        /// </summary>
        IPSEC_IKE_GETSPIFAIL = 13857,

        /// <summary>
        /// Given filter is invalid
        /// </summary>
        IPSEC_IKE_INVALID_FILTER = 13858,

        /// <summary>
        /// Memory allocation failed.
        /// </summary>
        IPSEC_IKE_OUT_OF_MEMORY = 13859,

        /// <summary>
        /// Failed to add Security Association to IPSec Driver.  The most common cause for this is if the IKE negotiation took too long to complete.  If the problem persists, reduce the load on the faulting machine.
        /// </summary>
        IPSEC_IKE_ADD_UPDATE_KEY_FAILED = 13860,

        /// <summary>
        /// Invalid policy
        /// </summary>
        IPSEC_IKE_INVALID_POLICY = 13861,

        /// <summary>
        /// Invalid DOI
        /// </summary>
        IPSEC_IKE_UNKNOWN_DOI = 13862,

        /// <summary>
        /// Invalid situation
        /// </summary>
        IPSEC_IKE_INVALID_SITUATION = 13863,

        /// <summary>
        /// Diffie-Hellman failure
        /// </summary>
        IPSEC_IKE_DH_FAILURE = 13864,

        /// <summary>
        /// Invalid Diffie-Hellman group
        /// </summary>
        IPSEC_IKE_INVALID_GROUP = 13865,

        /// <summary>
        /// Error encrypting payload
        /// </summary>
        IPSEC_IKE_ENCRYPT = 13866,

        /// <summary>
        /// Error decrypting payload
        /// </summary>
        IPSEC_IKE_DECRYPT = 13867,

        /// <summary>
        /// Policy match error
        /// </summary>
        IPSEC_IKE_POLICY_MATCH = 13868,

        /// <summary>
        /// Unsupported ID
        /// </summary>
        IPSEC_IKE_UNSUPPORTED_ID = 13869,

        /// <summary>
        /// Hash verification failed
        /// </summary>
        IPSEC_IKE_INVALID_HASH = 13870,

        /// <summary>
        /// Invalid hash algorithm
        /// </summary>
        IPSEC_IKE_INVALID_HASH_ALG = 13871,

        /// <summary>
        /// Invalid hash size
        /// </summary>
        IPSEC_IKE_INVALID_HASH_SIZE = 13872,

        /// <summary>
        /// Invalid encryption algorithm
        /// </summary>
        IPSEC_IKE_INVALID_ENCRYPT_ALG = 13873,

        /// <summary>
        /// Invalid authentication algorithm
        /// </summary>
        IPSEC_IKE_INVALID_AUTH_ALG = 13874,

        /// <summary>
        /// Invalid certificate signature
        /// </summary>
        IPSEC_IKE_INVALID_SIG = 13875,

        /// <summary>
        /// Load failed
        /// </summary>
        IPSEC_IKE_LOAD_FAILED = 13876,

        /// <summary>
        /// Deleted via RPC call
        /// </summary>
        IPSEC_IKE_RPC_DELETE = 13877,

        /// <summary>
        /// Temporary state created to perform reinit. This is not a real failure.
        /// </summary>
        IPSEC_IKE_BENIGN_REINIT = 13878,

        /// <summary>
        /// The lifetime value received in the Responder Lifetime Notify is below the Windows 2000 configured minimum value.  Please fix the policy on the peer machine.
        /// </summary>
        IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY = 13879,

        /// <summary>
        /// Key length in certificate is too small for configured security requirements.
        /// </summary>
        IPSEC_IKE_INVALID_CERT_KEYLEN = 13881,

        /// <summary>
        /// Max number of established MM SAs to peer exceeded.
        /// </summary>
        IPSEC_IKE_MM_LIMIT = 13882,

        /// <summary>
        /// IKE received a policy that disables negotiation.
        /// </summary>
        IPSEC_IKE_NEGOTIATION_DISABLED = 13883,

        /// <summary>
        /// ERROR_IPSEC_IKE_NEG_STATUS_END
        /// </summary>
        IPSEC_IKE_NEG_STATUS_END = 13884,
    }
}