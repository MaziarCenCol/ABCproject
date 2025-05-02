import plotly.express as px
import pandas as pd
import json

# Define file paths
json_file = "wwwroot/files/schedule.json"  # Ensure path matches your .NET app
output_file = "wwwroot/plots/plot.html"

def generate_plot():
    # Read JSON data
    with open(json_file, "r") as f:
        data = json.load(f)

    # Convert JSON to DataFrame
    df = pd.DataFrame(data)

    # Ensure Start/Finish columns are in datetime format
    df["Start"] = pd.to_datetime(df["Start"])
    df["Finish"] = pd.to_datetime(df["Finish"])

    # Create Gantt Chart (or Timeline)
    fig = px.timeline(df, 
                      x_start="Start", 
                      x_end="Finish", 
                      y="Machine", 
                      color="Job", 
                      title="Machine Schedule",
                      labels={"Machine": "Machines", "Task": "Tasks"},
                      hover_data=["Duration", "Job", "Require By"])

    fig.update_yaxes(categoryorder="total ascending")  # Sort Machines

    # Save the graph as HTML
    fig.write_html(output_file)

    # Print only the path for .NET (so it gets the correct file)
    print(output_file)

if __name__ == "__main__":
    generate_plot()
