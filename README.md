# argos
A.R.G.O.S.: Accessing Real Guest Operating Systems (in Unity)

# important note

If using the game released with the `custom-setup` script, the setup will prompt for a keyboard layout and encoding. Use `1` for both options :)

# prerequisites

This project already contains the necessary software for running the virtual environment (through Podman), **but requires virtualization to be enabled in your computer's UEFI BIOS** and **windows subsystem for linux (wsl2) windows feature to be enabled**

This must be done manually:

## Virtualization

Please refer to Microsoft's guide for enabling virtualization in your computer's UEFI BIOS: https://support.microsoft.com/en-us/windows/enable-virtualization-on-windows-c5578302-6e43-4b4b-a449-8ced115f58e1

## WSL2

Please refer to Microsoft's guide for enabling WSL2 windows feature: https://learn.microsoft.com/en-us/windows/wsl/install

# known issues

1. On displays that are not 1080p (1920x1080), the raw image of screen in the screen view does not change resolution, so it may look blurry.
2. Running into a wall or solid object stops movement entirely.

# license

This project is kept all rights reserved to Carrier Pigeon Dev.

This project redistributes Podman, licensed under Apache 2.0, obtained from the Windows exe installer here: https://github.com/containers/podman/releases/download/v5.6.2/podman-5.6.2-setup.exe.
