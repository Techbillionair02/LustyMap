LustyMap (Regime Edition) is a stabilized, minimal, compile-ready version of the popular Rust plugin LustyMap.
This version was rebuilt by Regime Gaming to serve as a safe foundation for customization and modern development.

The original LustyMap is large, outdated, and often fails to compile due to missing types, corrupted class structures, and unsupported modern C# features.
This skeleton edition:

Fixes every compile-breaking issue

Restores missing data models and classes

Removes unsupported new() shorthand (Mono incompatible)

Rebuilds configuration objects

Adds placeholder methods for imagery and UI

Preserves the architecture required for full LustyMap functionality

It does not include the full minimap/map rendering system — only the framework necessary to safely re-add those systems.

✨ Features in This Edition

✔ Fully compiles on modern Oxide/uMod
✔ Clean code structure for future expansion
✔ Config loads and regenerates correctly
✔ Correct class definitions for:

Map markers

Entity markers

Complex map mode

Minimap support
✔ Skeleton UI + CUI references
✔ Safe stubs for image validation
✔ No broken dependencies
✔ Developer-friendly foundation for adding back advanced features

🚧 What This Version Does NOT Include (By Design)

This is a skeleton, so the following systems are only stubs:

Automatic map image downloading

Minimap GUI rendering

Complex map tile handling

Real-time marker updates

Beancan/Custom imagery integration

Player icon tracking

These can be reintroduced safely using this framework.

🛠 Installation

Download LustyMap.cs

Upload to:

/oxide/plugins/


Restart the server or reload the plugin:

oxide.reload LustyMap


The plugin will generate a new config file at:

oxide/config/LustyMap.json

📁 Configuration

A full configuration file is automatically generated, including:

Friend/Clan support toggles

Marker visibility options

Minimap layout

Complex map settings

Spam protection

Image-handling options

These settings do not break even if features are not yet implemented.

🔧 Developer Notes

This rebuild is meant for developers who want to:

Rebuild LustyMap cleanly

Use it as a base for a custom map system

Add back UI/imagery features step-by-step

Ensure compatibility with future Rust updates

Remove bloat from the original version

Every major feature has a safe placeholder so functionality can be added modularly without re-breaking the plugin.

📄 Credits

Original Author: k1lly0u

Skeleton Repair & Modern Rebuild: Regime Gaming

📢 Support Regime Gaming

Website: Coming Soon
Discord: https://discord.gg/7JuSjMhuGQ

Steam Group: https://steamcommunity.com/groups/Regime_net
