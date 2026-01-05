from pyr8.formats.stars4 import Stars4Format
from pyr8.formats.key_settings import KeySettingsFormat

# stars4 = Stars4Format.from_file("./pyr8/tests/files/stars4.rn8")
# for i, entry in enumerate(stars4.entries):
#     print(f"Entry {i}: {entry}")

key_settings = KeySettingsFormat.from_file("./pyr8/tests/files/Run8KeySettings.r8")
for entry in key_settings.settings:
    print(f"Setting: {entry.name}, RD Button: {entry.rd_button}, Actions: ")
    for action in entry.actions:
        print(f"  - {action}")
