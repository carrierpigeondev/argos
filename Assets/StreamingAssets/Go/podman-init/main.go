package main

import (
	"errors"
	"flag"
	"fmt"
	"os"
	"os/exec"

	"github.com/klauspost/cpuid/v2"
)

var (
	podmanPath string = "..\\..\\RedHat\\Podman\\podman.exe"
	containerPath string = "..\\..\\debian-argos.oci.tar"
)

func checkVirtualization() {
	cpuid.Flags()
	flag.Parse()
	cpuid.Detect()

	if !(cpuid.CPU.Supports(cpuid.HYPERVISOR)) {
		fmt.Println("ERR: CPU does not support Virtualization must be first enabled in the UEFI/BIOS.")
		os.Exit(1)
	}
}

func checkCommandOK(command ...string) int {
	cmd := exec.Command(command[0], command[1:]...)
	cmd.Stdout = os.Stdout
	cmd.Stderr = os.Stderr
	if out, err := cmd.Output(); err != nil {
		var exiterr *exec.ExitError
		if errors.As(err, &exiterr) {
			exitcode := exiterr.ExitCode()
			fmt.Println(string(out), exitcode)
			return exitcode
		}

		fmt.Println(string(out))
		return 1
	}
	return 0
}

func makeWSLAccessible() {
	if checkCommandOK("wsl") == -1 {
		fmt.Println("LOG: Installing WSL... You may need to interact with a prompt...")
		if checkCommandOK("wsl --install") != 0 {
			fmt.Println("ERR: Could not install WSL")
			os.Exit(2)
		}
	}
}

func checkPodmanAccessible() {
	if checkCommandOK(podmanPath, "--version") != 0 {
		fmt.Println("ERR: Podman path specified in Go program inaccessible. Make sure you are running the executable in the right location.")
		os.Exit(3)
	}
}

func makePodmanUsable() {
	// initializes the machine correctly or has it already initialized
	// no wsl2 error as wsl2 is checked beforehand
	fmt.Println("LOG: Please note: seeing Error 125 in the following does not represent a fatal error, but rather than Podman is already working.")
	checkCommandOK(podmanPath, "machine", "init")  // no need to get error code
	checkCommandOK(podmanPath, "machine", "start")  // same as abovementioned
}

func startContainer() {
	checkCommandOK(podmanPath, "stop", "debian-argos")
	checkCommandOK(podmanPath, "rm", "-f", "debian-argos")
	checkCommandOK(podmanPath, "load", "-i", containerPath)
	checkCommandOK(podmanPath, "run", "-d", "--name", "debian-argos", "-p", "0.0.0.0:7681:7681", "debian-argos")
}

func main() {
	podmanPath = os.Args[1]
	containerPath = os.Args[2]
	fmt.Println(podmanPath)
	fmt.Println(containerPath)

	checkVirtualization()
	makeWSLAccessible()
	checkPodmanAccessible()
	makePodmanUsable()
	startContainer()
}