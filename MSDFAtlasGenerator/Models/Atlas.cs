﻿using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;

namespace MSDFAtlasGenerator.Models;

public partial class Atlas : ObservableObject
{
    [ObservableProperty]
    private AtlasType type;

    [ObservableProperty]
    private double distanceRange;

    [ObservableProperty]
    private double distanceRangeMiddle;

    [ObservableProperty]
    private double size;

    [ObservableProperty]
    private int width;

    [ObservableProperty]
    private int height;

    [ObservableProperty]
    private YDirection yOrigin;
}
