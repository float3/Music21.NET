{ pkgs ? import <nixpkgs> { } }:

pkgs.mkShell {
  buildInputs = with pkgs; [
    dotnet-sdk_8
    dotnet-runtime_8
    dotnet-aspnetcore_8
    msbuild
  ];

  shellHook = ''
  '';
}
