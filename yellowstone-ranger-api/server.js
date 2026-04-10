import express from "express";
import cors from "cors";
import dotenv from "dotenv";
import fetch from "node-fetch";

dotenv.config();

const app = express();
app.use(cors());
app.use(express.json());

const PORT = process.env.PORT || 3000;
const GEMINI_API_KEY = process.env.GEMINI_API_KEY;

// Basic health check
app.get("/health", (req, res) => {
  res.json({ ok: true, message: "Yellowstone Ranger API is running." });
});

app.post("/ask-ranger", async (req, res) => {
  console.log("ask-ranger hit:", req.body);

  try {
    const { eraName, question } = req.body ?? {};

    if (!eraName || !question) {
      return res.status(400).json({
        ok: false,
        error: "Missing eraName or question."
      });
    }

    const prompt = `
You are a Park Ranger at Yellowstone National Park during this era: ${eraName}.

Your job is to answer visitor questions about Yellowstone from the perspective of that era.
Focus especially on:
- history
- wildlife
- buildings and construction

Rules:
- Answer in 75 words or less.
- Stay historically grounded to the specified era.
- Be clear, friendly, and educational.
- If the visitor asks something outside the era, gently redirect the answer back to what is true in this era.

Visitor question:
${question}
`.trim();

    const response = await fetch(
      "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "x-goog-api-key": GEMINI_API_KEY
        },
        body: JSON.stringify({
          contents: [
            {
              parts: [
                { text: prompt }
              ]
            }
          ]
        })
      }
    );

    const data = await response.json();

    if (!response.ok) {
      console.error("Gemini API error:", data);
      return res.status(response.status).json({
        ok: false,
        error: data
      });
    }

    const answer =
      data?.candidates?.[0]?.content?.parts?.[0]?.text ??
      "Sorry, I could not generate an answer.";

    res.json({
      ok: true,
      answer
    });
  } catch (error) {
    console.error("ask-ranger error:", error);
    res.status(500).json({
      ok: false,
      error: "Server error while contacting Gemini."
    });
  }
});

app.listen(PORT, () => {
  console.log(`Yellowstone Ranger API listening on http://localhost:${PORT}`);
});