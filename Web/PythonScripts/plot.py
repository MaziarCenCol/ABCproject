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
    
    # Access the schedule property in the JSON
    if "schedule" not in data:
        print("Error: 'schedule' key not found in JSON data")
        return
    
    schedule_data = data["schedule"]
    
    # If schedule is an array of items
    if isinstance(schedule_data, list):
        df = pd.DataFrame(schedule_data)
    # If schedule contains nested properties you want as columns
    # elif isinstance(schedule_data, dict):
    #     # Option 1: Convert the dict directly if it's a dict of simple values
    #     df = pd.DataFrame([schedule_data])
    #     # Option 2: If it's a dict of arrays with equal length
    #     # df = pd.DataFrame({k: v for k, v in schedule_data.items() if isinstance(v, list)})
    else:
        print(f"Error: Unexpected schedule data format: {type(schedule_data)}")
        return

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
