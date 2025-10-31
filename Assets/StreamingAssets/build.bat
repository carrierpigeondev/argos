.\RedHat\Podman\podman.exe build -f .\Containerfile -t debian-argos:latest .
.\RedHat\Podman\podman.exe save --format oci-archive -o .\debian-argos.oci.tar debian-argos:latest