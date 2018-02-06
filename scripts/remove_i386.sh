echo "/usr/bin/lipo"
/usr/bin/lipo /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app/Contents/MonoBundle/libMonoPosixHelper.dylib -remove i386 -output /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app/Contents/MonoBundle/libMonoPosixHelper.dylib

echo "Target _CodesignNativeLibraries:"
    /usr/bin/codesign -v --force --sign EC1CB5386BE4DBBF9A2766A31366D28745E52AD7 /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app/Contents/MonoBundle/libMonoPosixHelper.dylib
echo  "Target _CodesignAppBundle:"
    /usr/bin/codesign -v --force --sign EC1CB5386BE4DBBF9A2766A31366D28745E52AD7 --entitlements /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/obj/x64/Release/Entitlements.xcent /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app
echo "Target _CodesignVerify:"
#    /usr/bin/codesign --verify -vvvv --deep bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app 
#    echo "bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app: valid on disk"
#    echo "bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app: satisfies its Designated Requirement"
echo "Target _CreateInstaller:"
#    Creating installer package
    /usr/bin/productbuild --component /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS.app /Applications --sign "3rd Party Mac Developer Installer: Eigil Krogh (4657Q2Y6NH)" /Users/eks/projects/xamarinProjs/TimeDateCalculator/timeDateCalculator/timeDateCalculator.macOS/bin/Release/timeDateCalculator.macOS.app/timeDateCalculator.macOS-1.2.pkg 
