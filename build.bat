@echo off
echo ================================
echo Building .NET project...
echo ================================

dotnet build

if %errorlevel% neq 0 (
    echo.
    echo Build failed!
    pause
    exit /b %errorlevel%
)

echo.
echo Build completed successfully!
pause
