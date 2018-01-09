#!/usr/bin/env bash
##########################################################################
# This is the Frosting bootstrapper script for Linux and OS X.
# Feel free to change this file to fit your needs.
##########################################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
DOT_NET_SDK_VERSION=2.1.3
TARGET_FRAMEWORK=netcoreapp2.0
###########################################################################
# INSTALL .NET CORE CLI
###########################################################################

echo "Installing .NET CLI..."
if [ ! -d "$SCRIPT_DIR/.dotnet" ]; then
  mkdir "$SCRIPT_DIR/.dotnet"
fi
curl -Lsfo "$SCRIPT_DIR/.dotnet/dotnet-install.sh" https://dot.net/v1/dotnet-install.sh
sudo bash "$SCRIPT_DIR/.dotnet/dotnet-install.sh" --version $DOT_NET_SDK_VERSION --install-dir .dotnet --no-path
export PATH="$SCRIPT_DIR/.dotnet":$PATH
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1

cd build
dotnet restore
dotnet publish -c Debug /v:q /nologo

###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Start Cake
exec "dotnet" "./bin/Debug/$TARGET_FRAMEWORK/publish/Build.dll" "$@"
#exec dotnet "$CAKE_DLL" "$@"
