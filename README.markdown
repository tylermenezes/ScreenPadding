# Purpose
This tool adds padding to the working area on Windows. This is useful, for example, if you're running Windows in a VM and want to leave
room for a VM toolbar at the top of the screen.

The working area is the area occupied by windows -- neither maximized nor windowed windows are allowed to exist beyond the working area,
except in special cases (e.g. the Windows Taskbar).

This tool behaves somewhat analogously to CSS padding, in that the desktop background will still extend beyond the padding.

# Use

Call screen-padding.exe with four arguments: the top, right, bottom, and left padding in pixels. This is similar to the CSS declaration for
padding, except that it should be specified unit-less; pixels are the only supported unit.

For example:

    screen-padding 60 15 10 30

Would cause the following padding to be applied:

    +----------------------------------------+
    |                  60px                  |
    |                                        |
    |                                        |
    |          +----------------------+      |
    |          |                      |      |
    |          |                      |      |
    |   30px   |                      | 15px |
    |          |                      |      |
    |          |                      |      |
    |          +----------------------+      |
    |                  10px                  |
    +----------------------------------------+
