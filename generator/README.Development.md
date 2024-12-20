# FAQs

## How to test modified Scand.StormPetrel.Generator code?

See details and execute the `build/build.ps1` script from the repository root directory or manually repeat script steps in Visual Studio or another IDE. Optional suggestions:
*  Use the `git clean -dfx` command to clean up the current directory from redundant output of previous builds. **CAUTION:** This command will remove ALL new files that are not yet in the Git tree.
*  The `git clean -dfx` command might fail due to locked log files. To fix this:
    * Find the process locking the file via Resource Monitor application:
        *  Go to the CPU tab.
        *  In the Associated Handles section, enter `StormPetrel` in the search input and press Enter.
    * Right-click on the found process and select `End Process`.
    * Repeat the `git clean -dfx` command.
