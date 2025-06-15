# AgentInvestigation

🔍 A tactical investigation game where you uncover the secrets of Iranian agents using sensor-based logic and deduction.

---

## 🎯 Game Concept

You've captured an Iranian agent — now it's time to uncover his weaknesses.

Attach sensors to the agent. Some will match his secret weakness profile.  
Your mission: find the correct combination and expose the agent.

---

## 🧱 Current Scope (MVP - Phase 1)

This version includes:

- 🕵️ A **Junior Iranian Agent** with a hidden list of 2 weaknesses (can be duplicates)
- ⚙️ A base **abstract Sensor** class, extended by concrete sensor types
- ✅ Matching logic that returns how many attached sensors match the agent’s weakness list (e.g., `1/2`, `2/2`)
- 🎮 Simple console gameplay loop until the agent is exposed

---

## 🧩 Core Classes

- `Agent` (abstract)  
  → Holds name, rank, and random list of weaknesses
- `JuniorTerrorist`  
  → Inherits from `Agent`, limited to 2 weaknesses
- `Sensor` (abstract)  
  → Defines the base for all sensor types with `Activate()` and `GetName()` methods
- `ThermalSensor`  
  → Inherits from `Sensor`, used in MVP
- `InvestigationManager`  
  → Runs the investigation loop and feedback system

---

## 🕹️ How It Works

1. Game starts with a randomly generated junior agent.
2. Player chooses which sensor to attach.
3. `Activate()` runs the logic and compares with the agent’s weakness list.
4. Player receives feedback (e.g., "1/2 matched").
5. Game ends when all required sensors are matched.

---

## 🧪 Example

Agent has weaknesses: `[Thermal, Thermal]`

- Turn 1: Attach `Thermal` → Feedback: `1/2`
- Turn 2: Attach another `Thermal` → Feedback: `2/2` → Agent exposed ✅

---

## 📁 Project Structure

